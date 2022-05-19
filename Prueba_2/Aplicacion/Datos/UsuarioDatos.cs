using Aplicacion.Models;
using MySql.Data.MySqlClient;
using MySql.Data;

public class UsuarioDatos{
    
    public List<UsuarioEmpleadoModel> allUsuario(){
        var oListaUsuarios = new List<UsuarioEmpleadoModel>();
        var con = new Conexion();
        using(var conexion = new MySqlConnection(con.getCadenaMySQL())){
            conexion.Open();
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = conexion;
            cmd.CommandText = 
                 "SELECT "
                +"  usuarios.Login, "
                +"  usuarios.Nombre, " 
                +"  usuarios.Paterno, " 
                +"  usuarios.Materno, " 
                +"  empleados.Sueldo, " 
                +"  date_format(empleados.FechaIngreso, '%d-%m-%Y') AS FechaIngreso " 
                +"FROM usuarios "
                +"LEFT JOIN empleados "
                +"ON empleados.Usuario = usuarios.Login";

            using(var dr = cmd.ExecuteReader()){
                while (dr.Read()){
                    Double result;
                    Double.TryParse(dr["Sueldo"].ToString() , out result);
                    oListaUsuarios.Add(new UsuarioEmpleadoModel(){
                        Login = dr["Login"].ToString(),
                        Nombre = dr["Nombre"].ToString(),
                        Paterno = dr["Paterno"].ToString(),
                        Materno = dr["Materno"].ToString(),
                        Sueldo = result,
                        FechaIngreso =  dr["FechaIngreso"].ToString()
                    });
                }
            }
        }

        return oListaUsuarios;
    }

    public UsuarioEmpleadoModel getUsuario(String userId){
        var oUsuario = new UsuarioEmpleadoModel();
        var con = new Conexion();
        using(var conexion = new MySqlConnection(con.getCadenaMySQL())){
            conexion.Open();
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = conexion;
            cmd.CommandText = 
                 "SELECT "
                +"  usuarios.Login, "
                +"  usuarios.Nombre, " 
                +"  usuarios.Paterno, " 
                +"  usuarios.Materno, " 
                +"  empleados.Sueldo, " 
                +"  date_format(empleados.FechaIngreso, '%Y-%m-%d') AS FechaIngreso " 
                +"FROM usuarios "
                +"LEFT JOIN empleados "
                +"ON empleados.Usuario = usuarios.Login "
                +"WHERE usuarios.Login = ?IdUsuario ";
            cmd.Parameters.Add("?IdUsuario", MySqlDbType.String).Value = userId;

            using(var dr = cmd.ExecuteReader()){
                while (dr.Read()){
                    Double result;
                    Double.TryParse(dr["Sueldo"].ToString() , out result);
                    oUsuario.Login = dr["Login"].ToString();
                    oUsuario.Nombre = dr["Nombre"].ToString();
                    oUsuario.Paterno = dr["Paterno"].ToString();
                    oUsuario.Materno = dr["Materno"].ToString();
                    oUsuario.Sueldo = result;
                    oUsuario.FechaIngreso = dr["FechaIngreso"].ToString();
                }
            }
        }

        return oUsuario;
    }

    public bool saveUsuario(UsuarioEmpleadoModel oUsuario){
        bool rpta = false;
        try{
            var con = new Conexion();
            
            using(var conexion = new MySqlConnection(con.getCadenaMySQL())){
                conexion.Open();
                MySqlCommand cmd = new MySqlCommand();
                MySqlCommand cmd2 = new MySqlCommand();
                cmd.Connection = conexion;
                cmd2.Connection = conexion;
                cmd.CommandText = 
                    "INSERT INTO usuarios "
                    +"(Login,Nombre,Paterno,Materno) VALUES("
                    +"  ?login,"
                    +"  ?nombre,"
                    +"  ?paterno,"
                    +"  ?materno "
                    +"); ";
                cmd2.CommandText = 
                    "INSERT INTO empleados "
                    +"(Usuario,Sueldo,FechaIngreso) VALUES ("
                    +"  ?login, "
                    +"  ?sueldo, "
                    +"  ?fechaingreso);";
                cmd.Parameters.Add("?login", MySqlDbType.String).Value = oUsuario.Login;
                cmd.Parameters.Add("?nombre", MySqlDbType.String).Value = oUsuario.Nombre;
                cmd.Parameters.Add("?paterno", MySqlDbType.String).Value = oUsuario.Paterno;
                cmd.Parameters.Add("?materno", MySqlDbType.String).Value = oUsuario.Materno;
                cmd2.Parameters.Add("?login", MySqlDbType.String).Value = oUsuario.Login;
                cmd2.Parameters.Add("?sueldo", MySqlDbType.Double).Value = oUsuario.Sueldo;
                cmd2.Parameters.Add("?fechaingreso", MySqlDbType.Date).Value = oUsuario.FechaIngreso;
                cmd.ExecuteNonQuery();
                cmd2.ExecuteNonQuery();
            }

            rpta = true;
        } catch(Exception e){
            string error = e.Message;
            rpta = false;
        }

        return rpta;
    }

    public bool editUsuario(UsuarioEmpleadoModel oUsuario){
        bool rpta = false;

        try{
            var con = new Conexion();
            using(var conexion = new MySqlConnection(con.getCadenaMySQL())){
                conexion.Open();
                MySqlCommand cmd = new MySqlCommand();
                MySqlCommand cmd2 = new MySqlCommand();
                cmd.Connection = conexion;
                cmd2.Connection = conexion;
                cmd.CommandText = 
                    "UPDATE usuarios SET"
                    +"  Nombre = ?nombre,"
                    +"  Paterno = ?paterno,"
                    +"  Materno = ?materno "
                    +"WHERE Login = ?idusuario;";
                cmd2.CommandText = 
                    "UPDATE empleados SET"
                    +"  Sueldo = ?sueldo,"
                    +"  FechaIngreso = ?fechaingreso "
                    +"WHERE Usuario = ?idusuario;";
                cmd.Parameters.Add("?idusuario", MySqlDbType.String).Value = oUsuario.Login;
                cmd.Parameters.Add("?nombre", MySqlDbType.String).Value = oUsuario.Nombre;
                cmd.Parameters.Add("?paterno", MySqlDbType.String).Value = oUsuario.Paterno;
                cmd.Parameters.Add("?materno", MySqlDbType.String).Value = oUsuario.Materno;
                cmd2.Parameters.Add("?sueldo", MySqlDbType.Double).Value = oUsuario.Sueldo;
                cmd2.Parameters.Add("?fechaingreso", MySqlDbType.Date).Value = oUsuario.FechaIngreso;
                cmd2.Parameters.Add("?idusuario", MySqlDbType.String).Value = oUsuario.Login;
                cmd.ExecuteNonQuery();
                cmd2.ExecuteNonQuery();
            }

            rpta = true;
        } catch(Exception e){
            string error = e.Message;
            Console.WriteLine(error);
            rpta = false;
        }

        return rpta;
    }
}