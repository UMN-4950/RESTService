using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RESTService.Models
{

    internal static class ModelExtensions
    {
        internal static UserDTO ToUserDTO(this User user)
        {
            return new UserDTO
            {
              Id=user.Id,
              FamilyName = user.FamilyName,
              GivenName = user.GivenName
            };
        }

        public static IList<UserDTO> ToUserDTO(this IList<User> users)
        {
            IList<UserDTO> newList = new List<UserDTO>();

            foreach (User kd in users)
            {
                UserDTO newItem = new UserDTO();
                newItem.Id = kd.Id;
                newItem.FamilyName = kd.FamilyName;
                newList.Add(newItem);
            }

            return newList;
        }

    }
}