// Copyright © 2008 Triamec Motion AG

using Triamec.Tama.Rlid19;
using Triamec.Tama.Vmid5;

namespace Triamec.Tam.Samples
{
    /// <summary>
    /// Tama program of the "Gear Up!" sample.
    /// </summary>
    [Tama]
    public static class ElectronicGearing
    {
        #region Fields

        static float _gear;
        static float _gearSquare;

        #endregion Fields

        #region Coupling function task
        /// <summary>
        /// The coupling function for the slave position is the sine of the master position.
        /// The transformation is described by following formulas for x_s(t), v_s(t) and a_s(t)
        /// - x_s(t) = gear * x_m(t)
        /// - v_s(t) = gear * v_m(t)
        /// - a_s(t) = gear^2 * a_m(t)
        ///
        /// Changing the gear is possible when the master's position is <c>0</c>.
        /// </summary>
        [TamaTask(Task.IsochronousMain)]
        static void CouplingFunction()
        {
            #region Change gear

            if (_gear != Register.Application.Variables.Floats[0])
            {
                // are the master and slave in a synchronized position where changing gear is possible?
                // change gear by reading from a Tama application register
                _gear = Register.Application.Variables.Floats[0];
                _gearSquare = _gear * _gear;
            }

            #endregion Change gear

            #region Slave motion

            // read in the master motion transmitted by subscription
            // write the slave motion to the path planner's coupling registers
            Register.Axes_0.Commands.PathPlanner.StreamX =
                _gear * Register.Axes_0.Commands.PathPlanner.Xnew;

            Register.Axes_0.Commands.PathPlanner.StreamV =
                _gear * Register.Axes_0.Commands.PathPlanner.Vnew;

            Register.Axes_0.Commands.PathPlanner.StreamA =
                _gearSquare * Register.Axes_0.Commands.PathPlanner.Anew;

            #endregion Slave motion
        }
        #endregion Coupling function task
    }
}
