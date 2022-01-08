using System.Globalization;
using System;

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
    
   public MensagemModel(string mensagem, TipoMensagem tipo=TipoMensagem.Informacao ){
       this.Tipo=tipo
       this.Texto=texto;

   }

}

}