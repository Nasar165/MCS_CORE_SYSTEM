
# Health Checks
## Intro
Writing health checks is an excellent way to make sure the API is working as intended
once in production. A health check report is accessed through HTTP  enabling easy 
maintenance and uptime monitored with a third party application.

  ## Common Server Responses
| Error Code | Description |
| :---: | --- |
| `200` | OK : Request was successful |
| `401` | Unauthorized : User not authenticated |
| `403` | Forbidden : User dont have the right privilages |
| `404` | Not found : The url does not exist or has been moved |
| `429` | Too many requests : To many requests within a x amount of time |
| `500` | Internal Server Error  : An error has occured server side |

## Comments
A developer committing code must only apply comments if necessary. Mainly because comments might become outdated, confusing other developers trying to improve an already exisitng test. To combat comments please choose good variable, method and class names instead of adding comments.

### Add HealtCheck
To add a Healt check you will need to do two things, first create a healt check 
and then the second register your healt check so that it is show in the API
response

### Write Healtcheck
To add a health check you will first need to create a file within api/healtchecks 
the filename and the class within must have the same name. You Class must inherit
the IHealtCheck Interfance see the [Sample](#sample) code for method name and in parameters
and return values. To register your healt Check visit 
[Register Healt cheack to serivce](#register-healtcheck-to-healtcheck-service)

#### Sample
``` csharp
namespace api.HealthChecks
{
    public sealed class CheckDatabase : IHealthCheck
    {
        public Task<HealthCheckResult> CheckHealthAsync(
                 HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            var sql = new NpgsqlConnection(AppConfigHelper.Instance.GetDbConnection());
            try
            {
                sql.Open();
                return Task.FromResult(HealthCheckResult.Healthy("Passed"));
            }
            catch
            {
                return Task.FromResult(HealthCheckResult.Unhealthy("Failed to Validate Database Connection."));
            }
            finally
            {
                sql.Close();
            }
        }
    }
}
```
## Register HealtCheck to HealtCheck Service
To register your newly created healt check, navigate to api/App_Start and open HealtCheckStarter.
within that fille go to InstallHealtChecks Metod and modify the service AddHelthCHecks method by
adding AddCheck<your class>("name of check");. Visit the [sample](#sample-1) to lean how to add 
 Add healt check
#### Sample
``` csharp
   public class HealthCheckStarter
    {
        public static void InstallHealthChecks(IServiceCollection service)
        {
            service.AddHealthChecks()
                .AddCheck<CheckDatabase>("Database Checkup")
                .AddCheck<FileWriterCheck>("FileWriter Check");
        }
    }
```

#### Sample API Response
| Info | Description |
| ------------- | ------------- |
| `Title` | Run HealtCheck |
| `Autentication` | Requred |
| `Url` | /healtcheck |
| `Method` | GET |
| `Success Response` | Status Code: 200 <br/> Response: (Sample API Response)[#sample-api-response]|
| `Error Response` | Status Code: 401 |

##### Sample API Response
``` yaml
  { 
    "Status": "Healthy",
      "Checks": [
          {
              "Status": "Healthy",
              "Component": "Database Checkup",
              "Description": "Passed",
              "Duration": "00:00:00.0064208"
          },
          {
              "Status": "Healthy",
              "Component": "FileWriter Check",
              "Description": "Passed",
              "Duration": "00:00:00.0557970"
          }
      ],
      "Duration": "00:00:00.0621342"
  }
```
