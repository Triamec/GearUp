$Id: readme.txt 36912 2021-02-16 08:14:04Z chm $
Copyright © 2008 Triamec Motion AG

Caution: you may harm your hardware when executing sample applications 
without adjusting configuration values to your hardware environment.
Please read and follow the recommendations below
before executing any sample application.

Overview
--------

This "Gear Up!" program is a .NET windows application
demonstrating the motion of two motors coupled with an electronic gearing,
using the Triamec Automation and Motion software (TAM Software).

This program shows you
- the use of subscriptions to communicate between two drives in real-time, and
- how to write a coupling function in C#/Tama.


Hardware requirements
---------------------

- a Tria-Link PCI adapter (TL100 or TLC201) mounted to your PC,
- at least two servo-drives connected to the Tria-Link,
- each one motor and one encoder connected to the servo-drives, resp.,
- power supply for the servo-drive logic and motor power.


Hardware configuration adjustment
---------------------------------

The file GearUpTamConfiguration.xml must be replaced by one
appropriate for your particular hardware environment.

It is recommended to use the TAM system explorer (part of the TAM Software)
as a convenient way to create your configuration file.
With the TAM system explorer, you can
- boot and identify your system;
- manipulate the register values of your drives;
- save the TAM configuration as an XML file.

It is clear that the adjustment of register values is a major task 
and requires understanding of motion control.
Please contact Triamec if you need further assistance with this setup procedure.
For this "Gear Up!" program, a working TAM configuration file is a prerequisite.

As an alternative to replacing the content of the existing GearUpTamConfiguration.xml, 
you can 
- add your own TAM configuration file to the GearUp project,
- make sure its "Copy to Output Directory" property is set to "Copy if newer", and
- change the application setting "TamConfigurationPath" to the name of your file.


What the program does
---------------------

After the above adjustments, you can start the program in the Visual Studio Debugger (F5-key).

After the GearUpForm has loaded,
- a TAM topology is created,
- the Tria-Link gets booted,
- the TAM configuration, including the Tama program for the electronic gearing, gets loaded and applied,
- the two servo-drives get searched and identified by their serial number.
After this, the program is ready for user interaction.

The user can
- enable and disable the position controller of the servo-drive,
- couple and decouple the slave and master servo-drive,
- move the master motor forth and back by units of a 1.5 revolutions (3 Pi),
- open the TAM system explorer as a child form.


Shortcomings
------------

While this program shows how to start programming with the TAM Software,
it neglects some general features that should be added
when using the sample code as a template for your own application:

- Activities of the business logic should always be executed
on a thread different from the application's main thread.
I.e. GUI event handling methods should call business methods asynchronously
and return immediately to keep the GUI responsive.

- A state machine should be implemented to properly model
the state of the application and its axes.
A model-view-control approach should be applied
to update the state of buttons and menus
in connection with this state machine.
Exception handling should be connected with the state machine
as well.