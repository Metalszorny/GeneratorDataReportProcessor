# GeneratorDataReportProcessor


### Description:
----------------
An XML file containing generator data is produced and provided as input into an input folder on a regular basis. 
The solution automatically picks up the received XML file as soon as it is placed in the input folder (location of input folder is set in the Application app.config file), perform parsing and data processing as appropriate to achieve the following:
1. It is required to calculate and output the Total Generation Value for each generator.
2. It is required to calculate and output the generator with the highest Daily Emissions for each day along with the emission value.
3. It is required to calculate and output Actual Heat Rate for each coal generator. 
The output is a single XML file into an output folder (location of output folder is set in the Application app.config file).  


### Used languages:
------------------
- C#
- XML


### Features:
------------
- Console
- Tasks/Theads
- Xml serialization & deserialization


### Enviroment:
--------------
- IDE: Microsoft Visual Studio 2019 Commutiny
- OS: Microsoft Windows 10 Ultimate, 64 bit


### Warning:
-----------
- The input, output and reference folders must be set in both the application's configuration and on the operation system.
- The reference folder must contain a reference data file.