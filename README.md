# Mockup-Api
Mockup application for report generation via REST API.  
Currently running on localhost on ports 5000 and 5001.  

Current endpoints:  
* __(GET)__ /api/reports/values - returns default values for the model class in json  
* __(GET)__ /api/reports/default - returns report filled with default values as pdf file 
* __(GET)__ /api/reports/template - returns unfilled report as pdf file
* __(POST)__ /api/reports/generate/json - requires body with object containing student info and report details, returns report filled with provided values  
  
Current data format:  
```
{  
   "student":{  
      "Name":"Dawid Suchy",  
      "Group":6,  
      "Section":2,  
      "Major":"Informatyka",  
      "TypeOfStudies":"SSI",  
      "Semester":7
   },
   "details":{
      "Subject":"TUC-a",
      "TopicNo":18,
      "TeacherName":"Henri Malysiak",
      "LabDate":"Czwartek, 8:45",
      "DeadlineDate":"2018-06-20T00:00:00"
   }
} 
```
  
Before running application remember to change template path in _MockReportGenerator_ class. 
