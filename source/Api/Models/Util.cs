using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System;
namespace Card.Models
{
    public class Util
    {
        public static int GetUserIdByToken(Controller controller)
        {
            string token = controller.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
            var jwtToken = new JwtSecurityToken(token);
            string subject = jwtToken.Subject;
            int userId = 0;
            if (!subject.Equals("") || subject != null)
            {
                try{
                    userId  = int.Parse(subject);
                }catch(Exception ex){

                }
            }
            return userId;
        }
    }
}