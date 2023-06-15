
// This file is used by Code Analysis to maintain SuppressMessage 
// attributes that are applied to this project.
// Project-level suppressions either have no target or are given 
// a specific target and scoped to a namespace, type, member, etc.

using System.Diagnostics.CodeAnalysis;

[assembly: SuppressMessage("Code Quality", "IDE0051:Remove unused private members", Justification = "Tama entry point", Scope = "member", Target = "~M:Triamec.Tam.Samples.ElectronicGearing.CouplingFunction")]
[assembly: SuppressMessage("Reliability", "CA2000:Dispose objects before losing scope", Justification = "Passed to Application.Run", Scope = "member", Target = "~M:Triamec.Tam.Samples.Program.Main")]
[assembly: SuppressMessage("Usage", "CA2213:Disposable fields should be disposed", Justification = "Passed to components", Scope = "member", Target = "~F:Triamec.Tam.Samples.GearUpForm._topology")]
[assembly: SuppressMessage("Usage", "CA2213:Disposable fields should be disposed", Justification = "Belongs to topology", Scope = "member", Target = "~F:Triamec.Tam.Samples.GearUpForm._system")]
[assembly: SuppressMessage("Usage", "CA2213:Disposable fields should be disposed", Justification = "Controlled by Couple/Decouple", Scope = "member", Target = "~F:Triamec.Tam.Samples.GearUpForm._subscription")]
[assembly: SuppressMessage("Usage", "CA2213:Disposable fields should be disposed", Justification = "Disposed in FormClosed handler", Scope = "member", Target = "~F:Triamec.Tam.Samples.GearUpForm._tamExplorerForm")]
