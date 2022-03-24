namespace ApiTest.Models;

public class EletricityMeter
{
	public int Id { get; set; }
	public string MeterNum { get; set; }
	public double value { get; set; }
	public DateTime TimeStamp { get; set; }	
}