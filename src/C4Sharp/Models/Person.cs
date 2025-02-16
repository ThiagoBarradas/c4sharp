﻿using C4Sharp.Models.Relationships;

namespace C4Sharp.Models
{
    /// <summary>
    /// A person represents one of the human users of your software system (e.g. actors, roles, personas, etc)
    /// <see href="https://c4model.com/"/>
    /// </summary>
    public sealed record Person(string Alias, string Label) : Structure(Alias, Label);
}