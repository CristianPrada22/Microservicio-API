using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Unillanos.ArquitecturaMS.Usuarios.Controllers
{
    //[Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {

        [HttpPost]
        [Route(("InsertarUsuario"))]
        public string InsertUsuario(UsuariosDto usuario)
        {
            ManejoInfo insertar = new ManejoInfo();
            return insertar.Write(usuario);
        }

        [HttpGet]
        [Route(("BuscarUsuario/{usuario}"))]
        public UsuariosDto ReadUsuario(string usuario)
        {
            ManejoInfo leer = new ManejoInfo();
            return leer.Read(usuario);
        }

        [HttpDelete]
        [Route(("EliminarUsuario/{usuario}"))]
        public string DeleteUsuario(string usuario)
        {
            ManejoInfo eliminar = new ManejoInfo();
            return eliminar.Delete(usuario);
        }

        [HttpPut]
        [Route(("actualizarUsuario"))]
        public string UpdateUsuario(UsuariosDto usuario)
        {
            ManejoInfo actualizar = new ManejoInfo();
            return actualizar.Update(usuario);
        }

    }

    public class ManejoInfo{

        public string Write(UsuariosDto usuario){

            List<UsuariosDto> usuarios= new List<UsuariosDto>();
            JSONReadAndWrite jInfo= new JSONReadAndWrite();
            usuarios= JsonConvert.DeserializeObject<List<UsuariosDto>>(jInfo.Read());

            if (usuarios == null){
                usuarios= new List<UsuariosDto>();
            }

            int comparar= 0;
            foreach (UsuariosDto InfoUsuario in usuarios){
                if (InfoUsuario.Nombre.Equals(usuario.Nombre)){
                    comparar= 1;
                    break;
                }
            }

            if (comparar == 1){
                usuarios.Add(usuario);
                string jsonString = JsonConvert.SerializeObject(usuarios);
                jInfo.Write(jsonString);
                return usuario.Nombre;
            }else{
                return "Este usuario ya está registrado en el sistema...";
            }
        }

        public UsuariosDto Read(string Usuario){

            List<UsuariosDto> usuarios = new List<UsuariosDto>();
            JSONReadAndWrite jInfo = new JSONReadAndWrite();
            usuarios = JsonConvert.DeserializeObject<List<UsuariosDto>>(jInfo.Read());

            foreach (UsuariosDto InfoUsuarios in usuarios){
                if (InfoUsuarios.Nombre.Equals(Usuario)){
                    return InfoUsuarios; ;
                }
            }
            return null;
        }

        public string Delete(String Usuario){

            List<UsuariosDto> usuarios = new List<UsuariosDto>();
            JSONReadAndWrite jInfo = new JSONReadAndWrite();
            usuarios = JsonConvert.DeserializeObject<List<UsuariosDto>>(jInfo.Read());

            int cuentaeliminar= 0;
            int contador = 0;
            foreach (UsuariosDto InfoUsuarios in usuarios){
                if (InfoUsuarios.Nombre.Equals(Usuario))
                {
                    usuarios.RemoveAt(contador);
                    cuentaeliminar= 1;
                    break;
                }
                contador= contador + 1;
            }

            if (cuentaeliminar == 1){
                string jsonString = JsonConvert.SerializeObject(usuarios);
                jInfo.Write(jsonString);
                return "El usuario se ha eliminado del sistema...";
            }
            return "No hay registrado un usuario con ese nombre...";
        }

        public string Update(UsuariosDto usuario){

            List<UsuariosDto> usuarios = new List<UsuariosDto>();
            JSONReadAndWrite jInfo = new JSONReadAndWrite();
            usuarios = JsonConvert.DeserializeObject<List<UsuariosDto>>(jInfo.Read());

            int comparar = 0;
            int contador = 0;
            foreach (UsuariosDto InfoUsuarios in usuarios){
                if (InfoUsuarios.Nombre.Equals(usuario.Nombre)){
                    comparar = 1;
                    break;
                }
                contador= contador + 1;
            }

            if (comparar==1){
                usuarios[contador] = usuario;
                string jsonString = JsonConvert.SerializeObject(usuarios);
                jInfo.Write(jsonString);
                return usuario.Nombre;
            }else{
                return "Este usuario no se encuentra en el sistema...";
            }
        }

    }

    public class JSONReadAndWrite{
        public JSONReadAndWrite() {}
        
        public string Read(){

            String jsonResult;
            string path = @"BD\TextFile.json";
            using (StreamReader stream = new StreamReader(path)){
                jsonResult = stream.ReadToEnd();
            }
            return jsonResult;
        }

        public void Write(string JSONString){
            
            string path = @"BD\TextFile.json";
            using (StreamWriter streamwr = new StreamWriter(path)){
                streamwr.WriteLine(JSONString);
            }
        }

    }

    public class UsuariosDto{
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Sexo { get; set; }
        public string Correo { get; set; }
        public string Telefono { get; set; }
        public string Edad { get; set; }
    }

}