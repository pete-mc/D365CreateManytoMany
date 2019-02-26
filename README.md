# Dynamics 365 - Azure Function Create N:N Relationships

This Azure Function was built to allow you to create N:N relationships from inside PowerApps. Please note this has been constructed with very basic auth methods to get a demo up and running quickly, please replace these with your orgs chosen authentication methods.

To get up and running:
1. Create a new Azure Function with the following settings:
 - Version 1 (To work with the Dynamics SDK)
 - C# HTTP Request
 - Anonymous Authorisation Level
 - Allowed HTTP methods POST
2. Copy / replace run.csx and projects.json
3. Replace the apikey value in line 18 with one of your choice (Or use a better method to identify clients)
4. Update line 24 with your CRM region (I was using CRM6 in this example)
5. Update line 25 & 26 with a CRM service account creds
6. Copy your fucntion URL for use in Flow
7. Import AssociateDisassociateRequest_20190226213220.zip as a new Flow 
8. Update the URL in the HTTP Step as per stpe 6
