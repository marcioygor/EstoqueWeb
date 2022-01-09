using System.Globalization;
using System;
using Newtonsoft.Json;

namespace ControleEstoque2.Models
{

  public enum TipoMensagem
  {
      Informacao,
      Erro
  }

  public class MensagemModel{
       public TipoMensagem Tipo {get;set;}
       public string Texto {get;set;}
    
   public MensagemModel(string texto, TipoMensagem tipo=TipoMensagem.Informacao){
       this.Tipo=tipo;
       this.Texto=texto;
   }

   public static string Serializar(string mensagem, TipoMensagem tipo=TipoMensagem.Informacao){       
       var mensagemModel=new MensagemModel(mensagem, tipo);
       return JsonConvert.SerializeObject(mensagemModel);
   }

    public static MensagemModel Desserializar(string mensagemString){
       return JsonConvert.DeserializeObject<MensagemModel>(mensagemString);
   }

}

}