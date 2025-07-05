using System;
using Entities;
public class CountryAddRequest
{
	public String? CountryName { get; set; }

	public Country ToCountry()
	{
		return new Country { CountryName = CountryName };
	}
}
