﻿namespace TaskWebApplication.Models
{
    public class ConsumeApiDto
    {
        public int userId { get; set; }
        public int id { get; set; }

        public string title { get; set; }
        public bool completed { get; set; }
    }
}
