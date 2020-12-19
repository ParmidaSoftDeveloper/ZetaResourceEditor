﻿namespace ExtendedControlsLibrary.General.UIUpdating
{
    using System;

    public class UpdateUIInformation
	{
        private readonly DateTime _createdAt = DateTime.Now;
        private readonly Guid _token = Guid.NewGuid();

        public UpdateUIInformation(object userState = null)
		{
			UserState = userState;
		}

		public object UserState { get; private set; }

        public Guid Token => _token;
        public DateTime CreatedAt => _createdAt;
    }
}