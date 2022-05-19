using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Aplicacion.Models;
using MySql.Data.MySqlClient;
using MySql.Data;
using System.Data;
using ClosedXML.Excel;
using System;
using System.IO;
using System.Text;
using CsvHelper;

namespace Aplicacion.Controllers;
public class UsuarioController : Controller {

    UsuarioDatos _UsuarioDatos = new UsuarioDatos();
    public IActionResult Listar(){
        //Mostrar Usuarios
        var oListaUsuarios = _UsuarioDatos.allUsuario();
        return View(oListaUsuarios);
    }

    public IActionResult Guardar(){
        //Solo devuelve la vista para guardar
        return View();
    }

    [HttpPost]
     public IActionResult Guardar(UsuarioEmpleadoModel oUsuario){
         //Guarda la informacion de los usuarios
         
         if(!ModelState.IsValid){
            return View();
         }

         var respuesta = _UsuarioDatos.saveUsuario(oUsuario);

         if(respuesta == true){
             return RedirectToAction("Listar");
         } else{
            return View();
         }
    }

    public IActionResult Editar(String Usuario){
        var oUsuario = _UsuarioDatos.getUsuario(Usuario);
        return View(oUsuario);
    }
    
    [HttpPost]
    public IActionResult Editar(UsuarioEmpleadoModel oUsuario){
        if(!ModelState.IsValid){
            return View();
        }

        var respuesta = _UsuarioDatos.editUsuario(oUsuario);
        
        if(respuesta == true){
             return RedirectToAction("Listar");
        } else{
            return View();
        }
    }

    public IActionResult Exportar(){
        DataTable tabla_usuarios = new DataTable();

        var con = new Conexion();
        using (var conexion = new MySqlConnection(con.getCadenaMySQL())){
            conexion.Open();
            using (var adapter = new MySqlDataAdapter()) {
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
                adapter.SelectCommand = cmd;

                adapter.Fill(tabla_usuarios);
            }
        }

       using( var libro = new XLWorkbook()){
           tabla_usuarios.TableName = "Usuarios";
           var hoja = libro.Worksheets.Add(tabla_usuarios);
           hoja.ColumnsUsed().AdjustToContents();

           using(var memoria = new MemoryStream()){
               libro.SaveAs(memoria);

               var nombreExcel = string.Concat("Reporte Usuario", DateTime.Now.ToString(),".xlsx");

               return File(memoria.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", nombreExcel);
           }
       }
    }


}