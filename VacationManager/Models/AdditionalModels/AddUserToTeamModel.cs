﻿namespace VacationManager.Models.AdditionalModels
{
    public class AddUserToTeamModel
    {
        public string? UserId { get; set; }
        public string? UserName { get; set; }
        public string Email { get; set; }
        public int TeamId { get; set; }
        public string? TeamName { get; set; }
    }
}
