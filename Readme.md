**Ufynd** 

**Project Setup**
- Component : Business specific logic
- Service: Client facing project
- Shared: Resources needed across the system

NOTE: 
- Tasks were all exposed as an api to easily verify solution.
- Json files were renamed according to task name
- Generated excel file(task 2) will be present in the api folder

**Running the project**

Run project as the usual .net project. Project should open on the url https://localhost:5001/swagger/index.html.

Proposal to optional question: On generating the excel file and stored in a cloud storage(s3, Azure file/blob, e.t.c), a job can be scheduled using tools like hangfire to run at time
X to pick the generated file with email and send to the specified email.