Rate Limiter Service (.NET 8)
Project Overview

This project implements a simple in-memory rate-limiting service built using ASP.NET Core (.NET 8).
The service exposes an API endpoint that determines whether a request from a given identifier (e.g., user ID or API key) should be allowed or rejected based on configurable rate-limiting rules.

The implementation is intentionally simple but designed with clean separation of concerns, making it easy to replace the in-memory storage with a distributed solution such as Redis in the future.

Tech Stack

.NET 8

ASP.NET Core Web API

In-memory storage (ConcurrentDictionary)

Token Bucket rate-limiting algorithm

xUnit for unit testing

Setup & Execution
Prerequisites

.NET SDK 8.0 or later

Git

(Optional) Postman or curl for testing

Verify .NET installation:

dotnet --version

Clone the Repository
git clone https://github.com/yashrathoredotnet-hub/Rate-Limiter.git
cd Rate-Limiter

Build the Application
dotnet build

Run the Application

Navigate to the API project folder and run:

cd Rate_Limiter
dotnet run


The application will start and listen on:

http://localhost:5000


You should see logs similar to:

Now listening on: http://localhost:5000
Application started. Press Ctrl+C to shut down.

Configuration

Rate-limiting rules are defined in appsettings.json:

{
  "RateLimiting": {
    "RequestsPerMinute": 100
  }
}


The configuration supports hot reload

Changes take effect without restarting the application

API Documentation
Check Rate Limit

Endpoint

POST /check


Request Body

{
  "identifier": "user1"
}


Responses

Status Code	Description
200 OK	Request allowed
429 Too Many Requests	Rate limit exceeded
400 Bad Request	Invalid or missing identifier
Testing the API
Using Postman

Create a POST request

URL:

http://localhost:5000/check


Headers:

Content-Type: application/json


Body:

{
  "identifier": "user1"
}


Send the request repeatedly:

Initial requests → 200 OK

After exceeding the limit → 429 Too Many Requests

Automated Tests

Unit tests validate the core rate-limiting logic independently of the API.

Run tests from the solution root:

dotnet test

Design Decisions & Trade-offs
Rate-Limiting Algorithm

Token Bucket was chosen because:

Allows controlled request bursts

Provides smoother traffic handling than fixed windows

Widely used in production systems

Trade-off: Slightly more complex than a fixed counter but more realistic.

In-Memory Storage

Uses a thread-safe ConcurrentDictionary

Abstracted behind an interface (IRateLimitStore)

Benefit:
The store can be replaced with Redis or Memcached without changing business logic.

Dependency Injection

All core components are registered via ASP.NET Core DI

Controllers depend only on abstractions

Distributed Systems Consideration

To support multiple instances behind a load balancer:

Replace in-memory store with Redis

Use atomic operations or Lua scripts to update tokens

Keep token buckets keyed by identifier

Improvements With More Time

Per-user or per-API rate limits

Sliding window algorithm

Metrics (allowed vs blocked requests)

Redis-based distributed store

Docker containerization

Conclusion

This project demonstrates:

Clean architecture

Practical system design

Thread-safe in-memory rate limiting

Testable and extensible code

It intentionally balances simplicity and correctness while leaving room for future scalability.
