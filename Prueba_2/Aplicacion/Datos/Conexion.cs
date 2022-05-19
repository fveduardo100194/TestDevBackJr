using MySql.Data.MySqlClient;
public class Conexion{

    private string cadenaMySQL = string.Empty;

    public Conexion(){
        var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").Build();

        cadenaMySQL = builder.GetSection("ConnectionStrings:CadenaMySQL").Value;
    }

    public string getCadenaMySQL(){
        return cadenaMySQL;
    }

}