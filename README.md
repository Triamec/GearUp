# Gear Up!

[![TAM - API](https://img.shields.io/static/v1?label=TAM&message=API&color=b51839)](https://www.triamec.com/en/tam-api.html)

This "Gear Up!" program is a .NET windows application
demonstrating the motion of two motors coupled with an electronic gearing,
using the Triamec Advanced Motion software (TAM Software).

This program shows you
- The use of subscriptions to communicate between two drives in real-time, and
- How to write a coupling function in C#/Tama.

*Caution: you may harm your hardware when executing sample applications 
without adjusting configuration values to your hardware environment.
Please read and follow the recommendations below
before executing any sample application.*

![TAM Acquisition](./doc/Acquisition_Movement.png)

## Hardware Prerequisites

- *Triamec* drive with a motor and encoder connected and configured with a stable position controller
- The sample assumes a configured axis which can be moved freely between -1 and 1 *[PositionController.PositionUnit]*
- Connection to the drive by *Tria-Link* (via PCI adapter), *USB* or *Ethernet*

- A *Tria-Link* PCI adapter (TL100 or TLC201) connected to your PC
- At least two *Triamec* drives with a motor and encoder connected and configured with a stable position controller
- A *Tria-Link* connection including the PCI adapter and the drives


## Hardware configuration adjustment

The file GearUpTamConfiguration.xml must be replaced by one
appropriate for your particular hardware environment.

It is recommended to use the TAM system explorer (part of the TAM Software)
as a convenient way to create your configuration file.
With the TAM system explorer, you can
- Boot and identify your system;
- Manipulate the register values of your drives;
- Save the TAM configuration as an XML file.

It is clear that the adjustment of register values is a major task 
and requires understanding of motion control.
Please contact Triamec if you need further assistance with this setup procedure.
For this "Gear Up!" program, a working TAM configuration file is a prerequisite.

As an alternative to replacing the content of the existing GearUpTamConfiguration.xml, 
you can 
- add your own TAM configuration file to the GearUp project,
- make sure its "Copy to Output Directory" property is set to "Copy if newer", and
- change the application setting "TamConfigurationPath" to the name of your file.

## Software Prerequisites

This project is made and built with [Microsoft Visual Studio](https://visualstudio.microsoft.com/en/).

In addition you need [TAM Software](https://www.triamec.com/en/tam-software-support.html) installation.

## Run the *Gear Up!* Application

1. Replace the GearUpTamConfiguration.xml as described above
2. Open the `Acquisition.sln`.
3. Open the `AcquisitionForm.cs` (view code)
4. Set the name of the axis for `AxisName`. Double check it in the register *Axes[].Information.AxisName* using the *TAM System Explorer*.
5. Set the name of the network interface card for `NicName`. You can find this name using the *TAM System Explorer*. In the example below, `NicName = "Ethernet 2"`.

![TAM Acquisition](./doc/Network_NicName.png)

5. Set `_moveAxis` to `true` if you want to use the trigger for the acquisition
6. Select the correct connection to the drive by using comment/uncomment for setting the `access` variable 

## Operate the *Gear Up!* Application

- Use the slider **Trigger** to adjust the velocity needed to start the acquisition. If `_moveAxis` is set to `false`, **Trigger** is ignored (continous acquisition)
- Use the slider **Recording time** to adjust the length of the acquisition

