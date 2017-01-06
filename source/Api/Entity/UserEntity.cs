using System;
using Microsoft.AspNetCore.Mvc;
namespace Card.Entity
{
    public class UserEntity
    {
        public UserEntity()
        {

        }
        public UserEntity(
            int Id,
            string fName,
            string lName,
            string code,
            string phoneNum,
            string emailAddress,
            string address,
            string town,
            string cif,
            string dob,
            string accBranch,
            string file
            )
        {
            this.Id = Id;
            this.fName = fName;
            this.lName = lName;
            this.code = code;
            this.phoneNum = phoneNum;
            this.emailAddress = emailAddress;
            this.address = address;
            this.town = town;
            this.cif = cif;
            this.dob = dob;
            this.accBranch = accBranch;
            this.file = file;
        }
        public UserEntity(string email, string password, int resetCode)
        {
            this.emailAddress = email;
            this.password = password;
            this.resetCode = resetCode;

        }
        public int Id { get; set; }
        public string fName { get; set; }
        public string lName { get; set; }
        public string code { get; set; }
        public string phoneNum { get; set; }

        public string emailAddress { get; set; }

        public string address { get; set; }

        public string town { get; set; }
        public string cif { get; set; }

        public string dob { get; set; }

        public string accBranch { get; set; }
        public string file { get; set; }

        public int resetCode { get; set; }
        public string password { get; set; }
    }
}