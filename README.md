##Spark IM Password Decrypter

This is a simple tool to decrypt saved passwords used by the Spark IM client. Seeing as Spark if often deployed within companies, and integrates with Active Directory for authentication - the stored credentials can be quite valuable during a pentest.

This tool was written to demonstrate the inherent weakness in the Spark saved password feature - and thus this feature should not be used.

###Usage

To gather passwords saved on a local system, just execute `sparkim-passview.exe` directly. For remote systems, just pass the workstation name - like this: `sparkim-passview.exe wk01`

When accessing remote systems, this application will attempt to use the remote administration share (C$), this will fail if you don't have access to this.

###Assumptions

This tool assumes that it will be run on Windows Vista or newer - older systems (such as Windows XP) use a different path structure. Supporting this would be fairly trivial, but it goes beyond my current needs.

This is a .NET project, so thus requires the framework to be installed to work. To maximize compatibility, it's targeting the 2.0 framework.

###Warning

This is a pentesting tool, as should only be used with proper authorization. Do not, ever, run this on a system you don't own, or have authorization to perform penetration tests against.
