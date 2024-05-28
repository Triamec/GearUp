// Copyright © 2008 Triamec Motion AG

using System;
using System.Windows.Forms;
using Triamec.Tam.Samples.Properties;
using Triamec.Tam.Subscriptions;
using Triamec.Tam.UI;
using Triamec.Tama;
using Triamec.TriaLink;
using RegisterMaster = Triamec.Tam.Rlid19.Register;
using RegisterSlave = Triamec.Tam.Rlid19.Register;

namespace Triamec.Tam.Samples {
    /// <summary>
    /// The main form of the TAM "Gear Up!" application.
    /// </summary>
    internal partial class GearUpForm : Form {
        #region Fields

        TamTopology _topology;
        TamSystem _system;
        TamLink _link;
        ITamDrive _masterDrive;
        TamAxis _masterAxis;
        RegisterMaster _masterRegisterRoot;
        ITamDrive _slaveDrive;
        TamAxis _slaveAxis;
        RegisterSlave _slaveRegisterRoot;

        ISubscription _subscription;

        float _velocityMaximum;

        TamExplorerForm _tamExplorerForm;

        #endregion Fields

        #region Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="GearUpForm"/> class.
        /// </summary>
        public GearUpForm() {
            InitializeComponent();
        }

        #endregion Constructor

        #region Gear up code
        /// <summary>
        /// Prepares the TAM system.
        /// </summary>
        /// <returns>Returns a <see langword="null"/> reference if the preparation succeeded;
        /// otherwise, the exception representing the failure.</returns>
        /// <exception cref="TamException">Startup failed.</exception>
        /// <remarks>
        /// 	<list type="bullet">
        /// 		<item><description>Creates a TAM topology,</description></item>
        /// 		<item><description>boots the Tria-Link,</description></item>
        /// 		<item><description>loads and applies a TAM configuration,</description></item>
        /// 		<item><description>searches for two servo-drives.</description></item>
        /// 	</list>
        /// </remarks>
        void Startup() {

            // Create the root object representing the topology of the TAM hardware.
            // We will dispose this object via components.
            _topology = new TamTopology();
            components.Add(_topology);

            // Add the local TAM system on this PC to the topology.
            _system = _topology.AddLocalSystem();

            // Get the (first) Tria-Link on the (first) PCI Adapter of the local TAM system.
            _link = _system[0][0];

            // Boot the Tria-Link so that it learns about connected stations.
            _link.Identify();

            // Caution!
            // Verify that the content of file GearUpTamConfiguration.xml
            // has been edited to apply to your hardware environment.
            // You can harm your hardware with inappropriate configuration values.
#if DEBUG
            System.Diagnostics.Debugger.Break();
#endif

            // Load the TAM configuration, showing progress in a dialog window.
            LoadSurveyor.Load(Settings.Default.TamConfigurationPath, _topology, autoStart: true, mainForm: this);

            // Find the two drives in the Tria-Link.
            // Iterate over the stations one by one
            // because the Tria-Link booting does not guarantee a particular order.
            foreach (TamStation station in _link) {
                if (station.HardwareSerialNumber == Settings.Default.SerialNumberMaster) {

                    // found the master drive to work with
                    _masterDrive = (ITamDrive)station.Device;

                    // Get its first (and only) axis of the found drive.
                    // Note: this must be done after loading the TAM configuration.
                    // setting the motor base configuration
                    _masterAxis = _masterDrive.Axes[0];

                    // Get the register tree of the master drive.
                    _masterRegisterRoot = (RegisterMaster)_masterDrive.Register;

                } else if (station.HardwareSerialNumber == Settings.Default.SerialNumberSlave) {

                    // found the slave drive to work with
                    _slaveDrive = (ITamDrive)station.Device;

                    // Get its first (and only) axis of the found drive.
                    // Note: this must be done after loading the TAM configuration.
                    // setting the motor base configuration
                    _slaveAxis = _slaveDrive.Axes[0];

                    // Get the register tree of the slave drive.
                    _slaveRegisterRoot = (RegisterSlave)_slaveDrive.Register;
                }
            }

            // Assert that we found the two drives
            if (_masterAxis == null) {
                throw new TamException("Failed to find the master drive.");
            }
            if (_slaveAxis == null) {
                throw new TamException("Failed to find the slave drive.");
            }

            // Download the Tama program
            // Note that alternatively, the Tama program may also be saved with the TAM configuration.
            var tamaManager = _slaveDrive.TamaManager;
            tamaManager.TamaAssemblyPath = TamaProgram.GetBinaryFileName(typeof(ElectronicGearing));
            tamaManager.DoDownload();

            // Read and cache the original velocity maximum value,
            // which was applied from the configuration file.
            _velocityMaximum = _masterRegisterRoot.Axes[0].Parameters.PathPlanner.VelocityMaximum.Read();
        }

        /// <exception cref="TamException">Enabling failed.</exception>
        void EnableDrives() {

            // set the master drive operational, i.e. switch the power section on
            _masterDrive.SetOperational();

            // set the slave drive operational, i.e. switch the power section on
            _slaveDrive.SetOperational();

            // reset any axis error and enable the axis controller
            _slaveAxis.Control(AxisControlCommands.ResetErrorAndEnable);

            // reset any axis error and enable the axis controller
            _masterAxis.Control(AxisControlCommands.ResetErrorAndEnable);
        }

        /// <exception cref="TamException">Disabling failed.</exception>
        void DisableDrives() {

            // disable the master axis controller
            _masterAxis.Control(AxisControlCommands.Disable);

            // on the master drive, switch the power section off
            _masterDrive.SwitchOff();

            // disable the slave axis controller
            _slaveAxis.Control(AxisControlCommands.Disable);

            // on the slabe drive, switch the power section off
            _slaveDrive.SwitchOff();
        }

        /// <summary>
        /// Couples the slave axis with the master axis.
        /// </summary>
        /// <exception cref="TamException">Coupling failed.</exception>
        void Couple() {

            // reset positions in order to avoid discontinuity in the slave's position control
            _masterAxis.SetPosition(0);
            _slaveAxis.SetPosition(0);

            // reset gear to ½
            _slaveRegisterRoot.Application.Variables.Floats[0].Write(0.5f);

            // enable electronic gearing function
            _slaveDrive.TamaManager.IsochronousVM.EnableAndVerify();

            // publish the path planner output of the master axis
            var publisher = new Publisher(
                // timestamp implicitly included
                _masterRegisterRoot.Axes[0].Signals.PathPlanner.Position,
                _masterRegisterRoot.Axes[0].Signals.PathPlanner.Velocity,
                _masterRegisterRoot.Axes[0].Signals.PathPlanner.Acceleration);

            // store the master motion signals to the path planner input of the slave axis
            var subscriber = new Subscriber(
                _slaveRegisterRoot.Axes[0].Commands.PathPlanner.StreamTimestamp,
                _slaveRegisterRoot.Axes[0].Commands.PathPlanner.Xnew,
                _slaveRegisterRoot.Axes[0].Commands.PathPlanner.Vnew,
                _slaveRegisterRoot.Axes[0].Commands.PathPlanner.Anew);

            // set up and enable subscription
            _subscription = _masterDrive.Station.Link.SubscriptionManager.Subscribe(publisher, subscriber);
            _subscription.Enable();

            // set slave into coupled move
            _slaveAxis.CoupleIn(false);
        }

        /// <summary>
        /// Decouples the slave axis from the master axis.
        /// </summary>
        /// <exception cref="TamException">Decoupling failed.</exception>
        void Decouple() {

            // stop coupled move of slave axis
            _slaveAxis.Stop();

            // stop subscription
            if (_subscription != null) {
                _subscription.Unsubscribe();
                _subscription.Dispose();
                _subscription = null;
            }
        }

        /// <summary>
        /// Moves in the specified direction.
        /// </summary>
        /// <param name="sign">A positive or negative value indicating the direction of the motion.</param>
        /// <exception cref="TamException">Moving failed.</exception>
        void MoveAxis(int sign) =>
            _masterDrive.Axes[0].MoveRelative(Math.Sign(sign) * 3 * Math.PI,
                                              _velocityMaximum * _velocityTrackBar.Value * 0.01f);

        #endregion Gear up code

        #region GUI handler methods
        #region Form handler methods

        /// <summary>
        /// Handles the Load event of the GearUpForm control.
        /// </summary>
        void GearUpForm_Load(object sender, EventArgs e) {
            try {
                Startup();
                _enableButton.Enabled = true;
                _disableButton.Enabled = true;

            } catch (TamException ex) {
                MessageBox.Show(this, ex.Message, Resources.StartupErrorCaption, MessageBoxButtons.OK,
                    MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, 0);
            }
        }

        /// <summary>
        /// Switches the bridge power off
        /// and exits the application.
        /// </summary>
        void Shutdown(object sender, EventArgs e) => Close();

        #endregion Form handler methods

        #region Button handler methods

        void OnEnableButtonClick(object sender, EventArgs e) {
            try {
                EnableDrives();

                _coupleButton.Enabled = true;
                _decoupleButton.Enabled = true;

            } catch (TamException ex) {
                MessageBox.Show(ex.Message, Resources.EnablingErrorCaption, MessageBoxButtons.OK,
                    MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, 0);
            }
        }

        void OnDisableButtonClick(object sender, EventArgs e) {
            try {
                DisableDrives();

                _moveNegativeButton.Enabled = false;
                _movePositiveButton.Enabled = false;
                _coupleButton.Enabled = false;
                _decoupleButton.Enabled = false;

            } catch (TamException ex) {
                MessageBox.Show(ex.Message, Resources.DisablingErrorCaption, MessageBoxButtons.OK,
                    MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, 0);
            }
        }

        void OnCoupleButtonClick(object sender, EventArgs e) {
            try {
                Couple();

                _moveNegativeButton.Enabled = true;
                _movePositiveButton.Enabled = true;
            } catch (TamException ex) {
                MessageBox.Show(ex.Message, Resources.CouplingErrorCaption, MessageBoxButtons.OK,
                    MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, 0);
            }
        }

        void OnDecoupleButtonClick(object sender, EventArgs e) {
            try {
                Decouple();

                _moveNegativeButton.Enabled = false;
                _movePositiveButton.Enabled = false;
            } catch (TamException ex) {
                MessageBox.Show(ex.Message, Resources.DecouplingErrorCaption, MessageBoxButtons.OK,
                    MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, 0);
            }
        }

        void OnMoveNegativeButtonClick(object sender, EventArgs e) {
            try {
                MoveAxis(-1);

            } catch (TamException ex) {
                MessageBox.Show(ex.Message, Resources.MoveErrorCaption, MessageBoxButtons.OK,
                    MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, 0);
            }

        }

        void OnMovePositiveButtonClick(object sender, EventArgs e) {
            try {
                MoveAxis(1);

            } catch (TamException ex) {
                MessageBox.Show(ex.Message, Resources.MoveErrorCaption, MessageBoxButtons.OK,
                    MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, 0);
            }

        }
        #endregion Button handler methods

        #region Menu handler methods
        void OnExplorerMenuItemClick(object sender, EventArgs e) {
            var menuItem = (ToolStripMenuItem)sender;

            void onExplorerClosed(object s, FormClosedEventArgs a) {

                // uncheck the menu item whenever the form is closed
                menuItem.Checked = false;

                var form = (Form)s;
                form.Dispose();
                form.FormClosed -= onExplorerClosed;
            }

            if ((_tamExplorerForm == null) || _tamExplorerForm.IsDisposed) {

                // Create the TAM system explorer as a child window.
                _tamExplorerForm = new TamExplorerForm {

                    // Skip loading the TAM configuration when the explorer opens
                    // because we already did this ourselves.
                    AutoLoadTamConfiguration = false,

                    // Tell the TAM system explorer the business object to work with.
                    Topology = _topology
                };

                _tamExplorerForm.FormClosed += onExplorerClosed;
            }

            // Toggle the display of the TAM system explorer window.
            _tamExplorerForm.Visible = menuItem.Checked;
        }
        #endregion Menu handler methods

        protected override void OnFormClosing(FormClosingEventArgs e) {
            const string message =
            "Do you want to disable the drives and close the program?";
            const string caption = "Exit application";
            var result = MessageBox.Show(message, caption,
                                         MessageBoxButtons.YesNo,
                                         MessageBoxIcon.Question);
            if (result == DialogResult.No) {
                // cancel the closure of the form.
                e.Cancel = true;
            } else {
                DisableDrives();
                base.OnFormClosing(e);
            }

        }


        #endregion GUI handler methods
    }
}
