# EasySave

# **Release Note: EasySave 3.0.0** 

## **General** 

Product: EasySave 

Software version number: 3.0.0  

Date of Release: December 21,2020 

 

## **Product Summary** 

Software that allows the user to create different save jobs. The saves can be incremental or complete, they can be launched one by one or all together. 

 

## **Related Documents** 

 

| **Title** | 
|:-----:|
| User guide |
| Technical documentation (model, viewmodel, view) |
| UML diagrams (Delivery0DiagramEasySave.pdf) |

 ## **New Features and Changes to Existing Functions** 
### **New Features/Functionality Added** 
- Backup in parallel
- Pause when business software detected
- Mono-instance

### **Features/Functionality Removed** 
 
### **Features/Functionality Modified** 
 - Differential backup



### **Issues Resolved** 
- Correction of a bug occures when the modification of a job. The modification was impossible

### **Known bugs**
- When copying files in a differential backup, some encrypted unmodified files are copied.
- When changing pages, the mono-instance isn't available. When you back to home page, the mono instance is available
 ## **Installation Notes and Restrictions**


 ## **Contents of this Version** 

### **Subsystems** 
Operating System: Win10, WinServer2019 

 

### **Modules** 

- .NET 3.1 

- Newtonsoft Json 
