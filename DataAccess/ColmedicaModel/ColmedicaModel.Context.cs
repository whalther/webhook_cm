﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DataAccess.ColmedicaModel
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class ColmedicaContext : DbContext
    {
        public ColmedicaContext()
            : base("name=ColmedicaContext")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<chatbotData> chatbotData { get; set; }
        public virtual DbSet<intentCategory> intentCategory { get; set; }
        public virtual DbSet<intentList> intentList { get; set; }
        public virtual DbSet<intentUnderstanding> intentUnderstanding { get; set; }
        public virtual DbSet<tempInfoAgendamiento> tempInfoAgendamiento { get; set; }
        public virtual DbSet<cmTipoDocumento> cmTipoDocumento { get; set; }
        public virtual DbSet<logErrorPeticion> logErrorPeticion { get; set; }
        public virtual DbSet<log_petitions> log_petitions { get; set; }
        public virtual DbSet<tempCiudades> tempCiudades { get; set; }
        public virtual DbSet<tempAuth> tempAuth { get; set; }
        public virtual DbSet<tempBeneficiarios> tempBeneficiarios { get; set; }
        public virtual DbSet<tempContratos> tempContratos { get; set; }
        public virtual DbSet<tempCitasBeneficiario> tempCitasBeneficiario { get; set; }
        public virtual DbSet<LogResultadosColmedica> LogResultadosColmedica { get; set; }
        public virtual DbSet<tempCita> tempCita { get; set; }
        public virtual DbSet<tempEspecialidades> tempEspecialidades { get; set; }
    
        public virtual int updateCita(string idConversacion, string campo, string valor)
        {
            var idConversacionParameter = idConversacion != null ?
                new ObjectParameter("idConversacion", idConversacion) :
                new ObjectParameter("idConversacion", typeof(string));
    
            var campoParameter = campo != null ?
                new ObjectParameter("campo", campo) :
                new ObjectParameter("campo", typeof(string));
    
            var valorParameter = valor != null ?
                new ObjectParameter("valor", valor) :
                new ObjectParameter("valor", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("updateCita", idConversacionParameter, campoParameter, valorParameter);
        }
    
        public virtual int cleanTablesConversation(string idConversacion)
        {
            var idConversacionParameter = idConversacion != null ?
                new ObjectParameter("idConversacion", idConversacion) :
                new ObjectParameter("idConversacion", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("cleanTablesConversation", idConversacionParameter);
        }
    
        public virtual int cleanTablesFlujo(Nullable<int> proceso, string idConversacion, string tabla)
        {
            var procesoParameter = proceso.HasValue ?
                new ObjectParameter("proceso", proceso) :
                new ObjectParameter("proceso", typeof(int));
    
            var idConversacionParameter = idConversacion != null ?
                new ObjectParameter("idConversacion", idConversacion) :
                new ObjectParameter("idConversacion", typeof(string));
    
            var tablaParameter = tabla != null ?
                new ObjectParameter("tabla", tabla) :
                new ObjectParameter("tabla", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("cleanTablesFlujo", procesoParameter, idConversacionParameter, tablaParameter);
        }
    
        public virtual int saveLinkCitaNoTemp(string idConversacion, Nullable<int> idCitaParam, string flag)
        {
            var idConversacionParameter = idConversacion != null ?
                new ObjectParameter("idConversacion", idConversacion) :
                new ObjectParameter("idConversacion", typeof(string));
    
            var idCitaParamParameter = idCitaParam.HasValue ?
                new ObjectParameter("idCitaParam", idCitaParam) :
                new ObjectParameter("idCitaParam", typeof(int));
    
            var flagParameter = flag != null ?
                new ObjectParameter("flag", flag) :
                new ObjectParameter("flag", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("saveLinkCitaNoTemp", idConversacionParameter, idCitaParamParameter, flagParameter);
        }
    
        public virtual int insertCitas(string jsonCitas, string idConv)
        {
            var jsonCitasParameter = jsonCitas != null ?
                new ObjectParameter("jsonCitas", jsonCitas) :
                new ObjectParameter("jsonCitas", typeof(string));
    
            var idConvParameter = idConv != null ?
                new ObjectParameter("idConv", idConv) :
                new ObjectParameter("idConv", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("insertCitas", jsonCitasParameter, idConvParameter);
        }
    }
}
