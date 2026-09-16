# 🎓 Student Management System – Backend

## 🚀 Overview

This is a backend application built using **ASP.NET Core Web API** that manages student data with full CRUD operations.
It also integrates with **Azure Functions** and **Azure Service Bus** for event-driven processing.

---

## 🧠 Features

* Create, Read, Update, Delete students
* RESTful API endpoints
* Integration with Azure Functions
* Service Bus Queue Trigger support
* Timer Trigger for background tasks

---

## 🛠️ Tech Stack

* ASP.NET Core Web API
* SQL Server (Stored Procedures)
* Azure Functions
* Azure Service Bus
* Azurite (local storage emulator)

---

## 🔗 API Endpoints

| Method | Endpoint          | Description       |
| ------ | ----------------- | ----------------- |
| GET    | /api/Student      | Get all students  |
| GET    | /api/Student/{id} | Get student by ID |
| POST   | /api/Student      | Add new student   |
| PUT    | /api/Student/{id} | Update student    |
| DELETE | /api/Student/{id} | Delete student    |

---

## ⚡ Azure Integration

* **HTTP Trigger** → Fetch student data
* **Timer Trigger** → Scheduled execution
* **Service Bus Trigger** → Processes queue messages asynchronously

---

## ▶️ Run Locally

```bash
dotnet run
```

---

## 📌 Architecture

React → API → Database

* Azure Functions (Event-driven layer)

---

## 💡 Key Learning

Implemented **event-driven architecture** using Azure Service Bus and Functions to decouple services and improve scalability.
