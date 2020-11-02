  _All extra tooling or applications that are required in order to be able run the application._
  - Operating system  
      _(version, patches,domain required?)_
  - Drivers, frameworks and Supporting Applications  
      _Not applicable_ 

      Name | Version | Manufacturer (url)
      --- | --- | ---
      IIS | 7.0 | Microsoft
      SQL Server | 2016 | Microsoft
      .Net Framework | 4.6.1 | Microsoft
 
  - Firewall settings  
      _Not applicable_  
        
      Description | Protocol | Portnumber
      --- | --- | --- | ---
      Web | HTTP | 80 
      Web | HTTPS | 443
      SQL Server | TCP | 1433  default port (http://support.microsoft.com/kb/287932)
      SQL Server Filestream | TCP | 139 and 445 default port (https://msdn.microsoft.com/en-us/library/dd283098.aspx)
  
- Application permissions
   _(Extreme example: one application could not work because the client could somehow not support the use of IFrames. This was not visible in any config and also not relying on a specific application)_
