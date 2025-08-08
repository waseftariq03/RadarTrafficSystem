# RadarTrafficSystem
This project is a .NET Core application that simulates a radar system for traffic violation detection and logging. The system demonstrates modern software architecture practices using dependency injection, background services, Entity Framework Core, and Serilog for structured logging.


# Radar Traffic System

This .NET Core application simulates a radar system for detecting traffic violations (speeding, wrong direction, emergency lane usage) and logs/records them into a SQL Server database.

## 🚀 How to Run

1. **Configure Database Connection**
   - Edit `appsettings.json`:
     ```json
     "ConnectionStrings": {
       "ConStr": "Server=YOUR_SERVER;Database=Test;User ID=USERNAME;Password=PASSWORD;Trusted_Connection=False;TrustServerCertificate=True"
     }
     ```
   - Make sure SQL Server is installed and running.

2. **Build & Run**
   - Open the solution in Visual Studio.
   - Set the startup project to `Struc`.
   - Press F5 or run:  
     ```
     dotnet run
     ```

3. **Logs & Data**
   - Logs are written to `Logs/log.txt`.
   - Detected violations are saved in the `Violations` table of your SQL Server database.

## 🛠️ Main Components

- **DAL**: Database context and entity models (in `DAL` project/folder)
- **Struc**: Application logic, background services, and DI setup
- **Logging**: Uses Serilog for structured file and console logging
- **Config**: All runtime settings are in `appsettings.json`

## 🧪 Testing

- **Unit tests** are in the `RadarTrafficSystem.Tests` project.
- To run tests:
