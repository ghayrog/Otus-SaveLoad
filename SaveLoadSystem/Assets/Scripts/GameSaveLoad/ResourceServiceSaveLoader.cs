using GameEngine;
using SaveSystem;
using System.Collections.Generic;
using System.Linq;

namespace GameSaveLoad
{
    public sealed class ResourceServiceSaveLoader : SaveLoader<ResourceServiceState, ResourceService>
    {
        protected override ResourceServiceState ConvertToData(ResourceService service)
        {
            ResourceServiceState resourceServiceState = new ResourceServiceState();
            Resource[] resources = service.GetResources().ToArray();

            resourceServiceState.resources = new ResourceState[resources.Length];

            for (int i = 0; i < resourceServiceState.resources.Length; i++)
            {
                resourceServiceState.resources[i] = new ResourceState
                {
                    id = resources[i].ID,
                    amount = resources[i].Amount
                };
            }

            return resourceServiceState;
        }

        protected override void SetupData(ResourceService service, ResourceServiceState data)
        {
            Dictionary<string, Resource> resources = service.GetResources().ToDictionary(x => x.ID, x => x);

            for (int i = 0; i < data.resources.Length; i++)
            {
                if (resources.TryGetValue(data.resources[i].id, out Resource resource))
                {
                    resource.Amount = data.resources[i].amount;
                }
            }

        }
    }
}
