using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace TravelExperts.Models
{
    public class Validate
    {
        private const string AgentKey = "validAgent";
        private const string UserKey = "validUser";
        private const string EmailKey = "validEmail";

        private ITempDataDictionary tempData { get; set; }
        public Validate(ITempDataDictionary temp) => tempData = temp;

        public bool IsValid { get; private set; }
        public string ErrorMessage { get; private set; }

        public void CheckAgent(string agentId, Repository<Agent> data)
        {
            Agent entity = data.Get(agentId);
            IsValid = (entity == null) ? true : false;
            ErrorMessage = (IsValid) ? "" : 
                $"Agent {entity.AgtFullName} is already in the database.";
        }
        public void MarkAgentChecked() => tempData[AgentKey] = true;
        public void ClearAgent() => tempData.Remove(AgentKey);
        public bool IsAgentChecked => tempData.Keys.Contains(AgentKey);

        public void CheckUser(string firstName, string lastName, string operation, Repository<User> data)
        {
            User entity = null;
            if (Operation.IsAdd(operation))
            {
                entity = data.Get(new QueryOptions<User>
                {
                    Where = a => a.Firstname == firstName && a.Lastname == lastName
                });
            }
            IsValid = (entity == null) ? true : false;
            ErrorMessage = (IsValid) ? "" :
                $"User {entity.Firstname} {entity.Lastname} is already in the database.";
        }
        public void MarkUserChecked() => tempData[UserKey] = true;
        public void ClearUser() => tempData.Remove(UserKey);
        public bool IsUserChecked => tempData.Keys.Contains(UserKey);
    }
}
