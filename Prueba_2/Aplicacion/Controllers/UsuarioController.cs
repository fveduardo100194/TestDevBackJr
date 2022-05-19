using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Aplicacion.Models;

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
        //Mostrar Usuarios
        /*var oListaUsuarios = _UsuarioDatos.allUsuario();
        return View(oListaUsuarios);

        for (int i = 0; i &amp;lt; ilength; i++)
            sbOutput.AppendLine(string.Join(strSeperator, inaOutput[i]));
 
        // Create and write the csv file
        File.WriteAllText(strFilePath, sbOutput.ToString());
 
        // To append more lines to the csv file
        File.AppendAllText(strFilePath, sbOutput.ToString());
        }*/
    }


}