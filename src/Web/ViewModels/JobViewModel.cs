using System;
using System.Collections.Generic;
using ApplicationCore.Constants;
using ApplicationCore.Entities;

namespace Web.ViewModels
{
    public class JobViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Status { get; set; }
        public int? Floor { get; set; }
        public int? Room { get; set; }
        public string RoomTypeName { get; set; }
        public int? RJobID { get; set; }
        public string Color { get; set; }
        public bool AbleToMarkAsComplete { get; set; }

        public static JobViewModel ConvertFrom(Job job) => new()
        {
            Floor = job.Floor,
            Id = job.Id,
            Name = job.Name,
            Room = job.Room,
            Status = job.Status,
            RJobID = job.RJobID,
            RoomTypeName = job.RoomType?.Name,
            AbleToMarkAsComplete = job.AbleToMarkAsComplete,
            Color = ColorPicker[job.Status]
        };

        private static readonly Dictionary<string, string> ColorPicker = new()
        {
            {
                JobStatus.Delayed, "red"
            },
            {
                JobStatus.NotStarted, "gray"
            },
            {
                JobStatus.InProgress, "gold"
            },
            {
                JobStatus.Complete, "green"
            }
        };
    }
}