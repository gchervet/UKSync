using System;
using System.Collections.Generic;
using System.Dynamic;
using MoodleWS.Common.Entities;
using MoodleWS.Common.Enums;
using MoodleWS.Common.Exceptions;
using MoodleWS.Domain.BusinessLogic;

namespace MoodleWS
{
    public class MoodleOperations
    {
        #region - MoodleWSMethods -

        private MoodleWSMethods _moodleWSMethods;

        private MoodleWSMethods MoodleWSMethods
        {
            get { if (this._moodleWSMethods == null) this._moodleWSMethods = new MoodleWSMethods(); return this._moodleWSMethods; }
        }

        #endregion - MoodleWSMethods -

        #region - MoodleWSBusiness -

        private MoodleWSBusiness _moodleWSBusiness;

        private MoodleWSBusiness MoodleWSBusiness
        {
            get { if (this._moodleWSBusiness == null) this._moodleWSBusiness = new MoodleWSBusiness(); return this._moodleWSBusiness; }
        }

        #endregion - MoodleWSBusiness -

        #region - Methods -

        public bool AltaCursos()
        {
            bool hayErrores = false;

            List<UniMoodleWSCursosDTO> cursosAlta = this.MoodleWSBusiness.ObtenerCursos();

            if (cursosAlta != null && cursosAlta.Count > 0)
            {
                foreach (UniMoodleWSCursosDTO curso in cursosAlta)
                {
                    dynamic course = new ExpandoObject();
                    course.fullname = curso.Nombre;
                    course.shortname = curso.NombreCorto;
                    course.categoryid = curso.MoodleIdCategoria;
                    course.summary = curso.Descripcion;
                    course.format = ConfigEnum.MoodleCourseFormat;
                    course.idnumber = curso.IdNumber;
                    course.startdate = ConfigEnum.CursoFechaInicio;
                    course.numsections = ConfigEnum.OpcionCursoCantidadTemas;
                    course.coursedisplay = ConfigEnum.OpcionCursoPaginacion;

                    try
                    {
                        dynamic cursoCreado = this.MoodleWSMethods.CreateCourse(course);
                        Int64 moodleCursoId = Convert.ToInt64(cursoCreado[0].id);
                        this.MoodleWSBusiness.ActualizarCurso(curso.IdNumber, moodleCursoId, (int)EstadoEnum.PROCESADO, null);
                    }
                    catch (MoodleWSException ex)
                    {
                        this.MoodleWSBusiness.ActualizarCurso(curso.IdNumber, 0, (int)EstadoEnum.CON_ERROR, ex.Message);

                        hayErrores = true;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                        Console.WriteLine(ex.StackTrace);

                        hayErrores = true;
                    }
                }
            }

            return hayErrores;
        }

        public bool AltaGrupos()
        {
            bool hayErrores = false;

            List<UniMoodleWSGruposDTO> gruposAlta = this.MoodleWSBusiness.ObtenerGrupos();

            if (gruposAlta != null && gruposAlta.Count > 0)
            {
                foreach (UniMoodleWSGruposDTO grupo in gruposAlta)
                {
                    dynamic group = new ExpandoObject();
                    group.courseid = grupo.MoodleIdCurso;
                    group.name = grupo.Nombre;
                    group.description = grupo.Descripcion;
                    group.idnumber = grupo.IdNumber;

                    try
                    {
                        dynamic grupoCreado = this.MoodleWSMethods.CreateGroup(group);
                        Int64 moodleGrupoId = Convert.ToInt64(grupoCreado[0].id);
                        this.MoodleWSBusiness.ActualizarGrupo(grupo.IdNumber, moodleGrupoId, (int)EstadoEnum.PROCESADO, null);
                    }
                    catch (MoodleWSException ex)
                    {
                        this.MoodleWSBusiness.ActualizarGrupo(grupo.IdNumber, 0, (int)EstadoEnum.CON_ERROR, ex.Message);

                        hayErrores = true;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                        Console.WriteLine(ex.StackTrace);

                        hayErrores = true;
                    }
                }
            }

            return hayErrores;
        }

        public bool AltaUsuarios()
        {
            bool hayErrores = false;

            List<UniMoodleWSUsuariosDTO> usuariosAlta = this.MoodleWSBusiness.ObtenerUsuarios();

            if (usuariosAlta != null && usuariosAlta.Count > 0)
            {
                foreach (UniMoodleWSUsuariosDTO usuario in usuariosAlta)
                {
                    dynamic user = new ExpandoObject();
                    user.username = usuario.Username;
                    user.firstname = usuario.Nombre;
                    user.lastname = usuario.Apellido;
                    user.email = usuario.Mail;
                    user.auth = ConfigEnum.MoodleUserAuthMethod;
                    user.password = ConfigEnum.MoodleUserPasswordDefault;
                    user.idnumber = usuario.IdNumber;

                    try
                    {
                        dynamic usuarioCreado = this.MoodleWSMethods.CreateUser(user);
                        Int64 moodleUserId = Convert.ToInt64(usuarioCreado[0].id);
                        this.MoodleWSBusiness.ActualizarUsuario(usuario.IdNumber, moodleUserId, (int)EstadoEnum.PROCESADO, null);
                    }
                    catch (MoodleWSException ex)
                    {
                        this.MoodleWSBusiness.ActualizarUsuario(usuario.IdNumber, 0, (int)EstadoEnum.CON_ERROR, ex.Message);

                        hayErrores = true;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                        Console.WriteLine(ex.StackTrace);

                        hayErrores = true;
                    }
                }
            }

            return hayErrores;
        }

        public bool ModificacionUsuarios()
        {
            bool hayErrores = false;

            List<UniMoodleWSUsuariosModificarDTO> usuariosModificacion = this.MoodleWSBusiness.ObtenerUsuariosModificar();

            if (usuariosModificacion != null && usuariosModificacion.Count > 0)
            {
                foreach (UniMoodleWSUsuariosModificarDTO usuario in usuariosModificacion)
                {
                    dynamic user = new ExpandoObject();
                    user.id = usuario.MoodleId;
                    /*user.username = usuario.Username;
                    user.firstname = usuario.Nombre;
                    user.lastname = usuario.Apellido;
                    user.email = usuario.Mail;
                    user.auth = ConfigEnum.MoodleUserAuthMethod;
                    user.password = ConfigEnum.MoodleUserPasswordDefault;*/
                    user.idnumber = usuario.IdNumber;

                    try
                    {
                        dynamic usuarioModificado = this.MoodleWSMethods.UpdateUser(user);
                        
                        //Int64 moodleUserId = Convert.ToInt64(usuarioCreado[0].id);
                        //this.MoodleWSBusiness.ActualizarUsuario(usuario.IdNumber, moodleUserId, (int)EstadoEnum.PROCESADO, null);
                    }
                    catch (MoodleWSException ex)
                    {
                        //this.MoodleWSBusiness.ActualizarUsuario(usuario.IdNumber, 0, (int)EstadoEnum.CON_ERROR, ex.Message);

                        hayErrores = true;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                        Console.WriteLine(ex.StackTrace);

                        hayErrores = true;
                    }
                }
            }

            return hayErrores;
        }

        public bool AltaMatriculaciones()
        {
            bool hayErrores = false;

            List<UniMoodleWSMatriculacionesDTO> matriculacionesAlta = this.MoodleWSBusiness.ObtenerMatriculaciones(ConfigEnum.OperacionAlta);

            if (matriculacionesAlta != null && matriculacionesAlta.Count > 0)
            {
                foreach (UniMoodleWSMatriculacionesDTO matri in matriculacionesAlta)
                {
                    dynamic enrolment = new ExpandoObject();
                    enrolment.userid = matri.MoodleIdUsuario;
                    enrolment.courseid = matri.MoodleIdCurso;
                    enrolment.roleid = matri.MoodleIdRol;

                    try
                    {
                        dynamic usuarioMatriculado = this.MoodleWSMethods.EnrolUser(enrolment);

                        if (usuarioMatriculado == null || usuarioMatriculado.exception == null)
                        {
                            this.MoodleWSBusiness.ActualizarMatriculacion(matri.Clave, (int)EstadoEnum.PROCESADO, null);
                        }
                    }
                    catch (MoodleWSException ex)
                    {
                        this.MoodleWSBusiness.ActualizarMatriculacion(matri.Clave, (int)EstadoEnum.CON_ERROR, ex.Message);

                        hayErrores = true;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                        Console.WriteLine(ex.StackTrace);

                        hayErrores = true;
                    }
                }
            }

            return hayErrores;
        }

        public bool AltaAgrupamientos()
        {
            bool hayErrores = false;

            List<UniMoodleWSAgrupamientosDTO> agrupamientosAlta = this.MoodleWSBusiness.ObtenerAgrupamientos(ConfigEnum.OperacionAlta);

            if (agrupamientosAlta != null && agrupamientosAlta.Count > 0)
            {
                foreach (UniMoodleWSAgrupamientosDTO agrupamiento in agrupamientosAlta)
                {
                    dynamic addGroupMember = new ExpandoObject();
                    addGroupMember.groupid = agrupamiento.MoodleIdGrupo;
                    addGroupMember.userid = agrupamiento.MoodleIdUsuario;

                    try
                    {
                        dynamic usuarioAgrupado = this.MoodleWSMethods.AddGroupMember(addGroupMember);

                        if (usuarioAgrupado == null || usuarioAgrupado.exception == null)
                        {
                            this.MoodleWSBusiness.ActualizarAgrupamiento(agrupamiento.Clave, (int)EstadoEnum.PROCESADO, null);
                        }
                    }
                    catch (MoodleWSException ex)
                    {
                        this.MoodleWSBusiness.ActualizarAgrupamiento(agrupamiento.Clave, (int)EstadoEnum.CON_ERROR, ex.Message);

                        hayErrores = true;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                        Console.WriteLine(ex.StackTrace);

                        hayErrores = true;
                    }
                }
            }

            return hayErrores;
        }

        public bool BajaMatriculaciones()
        {
            bool hayErrores = false;

            List<UniMoodleWSMatriculacionesDTO> matriculacionesBaja = this.MoodleWSBusiness.ObtenerMatriculaciones(ConfigEnum.OperacionBaja);

            if (matriculacionesBaja != null && matriculacionesBaja.Count > 0)
            {
                foreach (UniMoodleWSMatriculacionesDTO matri in matriculacionesBaja)
                {
                    dynamic unenrolment = new ExpandoObject();
                    unenrolment.userid = matri.MoodleIdUsuario;
                    unenrolment.courseid = matri.MoodleIdCurso;

                    try
                    {
                        dynamic usuarioDesmatriculado = this.MoodleWSMethods.UnenrolUser(unenrolment);

                        if (usuarioDesmatriculado == null || usuarioDesmatriculado.exception == null)
                        {
                            this.MoodleWSBusiness.ActualizarMatriculacion(matri.Clave, (int)EstadoEnum.PROCESADO, null);
                        }
                    }
                    catch (MoodleWSException ex)
                    {
                        this.MoodleWSBusiness.ActualizarMatriculacion(matri.Clave, (int)EstadoEnum.CON_ERROR, ex.Message);

                        hayErrores = true;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                        Console.WriteLine(ex.StackTrace);

                        hayErrores = true;
                    }
                }
            }

            return hayErrores;
        }

        public bool BajaAgrupamientos()
        {
            bool hayErrores = false;

            List<UniMoodleWSAgrupamientosDTO> agrupamientosBaja = this.MoodleWSBusiness.ObtenerAgrupamientos(ConfigEnum.OperacionBaja);

            if (agrupamientosBaja != null && agrupamientosBaja.Count > 0)
            {
                foreach (UniMoodleWSAgrupamientosDTO agrupamiento in agrupamientosBaja)
                {
                    dynamic deleteGroupMember = new ExpandoObject();
                    deleteGroupMember.groupid = agrupamiento.MoodleIdGrupo;
                    deleteGroupMember.userid = agrupamiento.MoodleIdUsuario;

                    try
                    {
                        dynamic usuarioDesagrupado = this.MoodleWSMethods.DeleteGroupMember(deleteGroupMember);

                        if (usuarioDesagrupado == null || usuarioDesagrupado.exception == null)
                        {
                            this.MoodleWSBusiness.ActualizarAgrupamiento(agrupamiento.Clave, (int)EstadoEnum.PROCESADO, null);
                        }
                    }
                    catch (MoodleWSException ex)
                    {
                        this.MoodleWSBusiness.ActualizarAgrupamiento(agrupamiento.Clave, (int)EstadoEnum.CON_ERROR, ex.Message);

                        hayErrores = true;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                        Console.WriteLine(ex.StackTrace);

                        hayErrores = true;
                    }
                }
            }

            return hayErrores;
        }

        public dynamic ObtenerUsuarioPorUsername(string username)
        {
            dynamic usuario = this.MoodleWSMethods.GetUserByUsername(username);

            if (usuario.Count > 0)
            {
                return usuario;
            }
            else
            {
                return null;
            }
        }

        public dynamic ObtenerUsuarioPorIdNumber(int idNumber)
        {
            dynamic usuario = this.MoodleWSMethods.GetUserByIdNumber(idNumber);

            if (usuario.Count > 0)
            {
                return usuario;
            }
            else
            {
                return null;
            }
        }

        public dynamic ObtenerUsuarioSegunCriterio(string criterio, object valor)
        {
            dynamic usuario = this.MoodleWSMethods.GetUserByCriteria(criterio, valor.ToString());

            if (usuario.Count > 0)
            {
                return usuario;
            }
            else
            {
                return null;
            }
        }

        public dynamic ObtenerUsuarioPorId(long id)
        {
            dynamic usuario = this.MoodleWSMethods.GetUserById(id);

            if (usuario.Count > 0)
            {
                return usuario;
            }
            else
            {
                return null;
            }
        }

        public dynamic ObtenerCategoriaPorCriterio(string criterio, object valor)
        {
            dynamic categoria = this.MoodleWSMethods.GetCategoryByCriteria(criterio, valor.ToString());

            if (categoria.Count > 0)
            {
                return categoria;
            }
            else
            {
                return null;
            }
        }

        public dynamic ObtenerCursoPorNombre(string nombreLargo)
        {
            dynamic curso = this.MoodleWSMethods.GetCourseByName(nombreLargo);

            if (curso.Count > 0)
            {
                return curso;
            }
            else
            {
                return null;
            }
        }

        #endregion - Methods -
    }
}
