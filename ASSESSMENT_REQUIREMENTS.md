# Assessment Requirements

The following requirements were provided as part of the technical assessment.

---

Hi Ndiphiwe,

It was nice meeting you. 😊

Here is a test project that will give an idea what we do. (see attached)

If it works well, we will get you in to walk us through your solution.

Please upload to git and share the repository link.

## Requirements

### Restful API

Design and implement a RESTful API using **C# .Net Core (version 9)**.

---

### Swagger Documentation

Integrate **Swagger** to generate API documentation automatically.

Ensure that all endpoints, request parameters, and responses are well-documented using Swagger annotations.

---

### 3. Database CRUD Layer with MSSQL

(For ORM. **No EF or Linq**, please use **ADO or Dapper**)

Implement CRUD operations (**Create, Read, Update, Delete**) for the database.

Utilize **MSSQL** as the database backend to store and retrieve data.

---

### Unit Tests

Add appropriate **unit tests** to ensure sufficient coverage.

---

### Additional Questions

a. What would you do to **enhance performance** of your project?

b. What **additional enhancements** would you add to the project to make it more complete?

---

### Microservice Scenario

You are creating a **microservice that relates to sensitive words**.

Another system or microservice will send your microservice a string and your service must return the **sanitized string**.

Example:

Request to your API:

```json
{
  "Input": "You need to create a string"
}
```

Response from your API:

```json
{
  "Output": "You need to ****** a string"
}
```

The rest of the **CRUD layer** will be used to manage the sensitive words.

---

### Notes

* Show your **skill level** in your solution.
* You have **7 days** to complete the task.

Have fun with the solution 😊
