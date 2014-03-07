#region COPYRIGHT(C) 2014 NACIONAL SOFT LTDA
/*****************************************************************************
{                                                                            }
{    COPYRIGHT (C) 2013-2014 NACIONAL SOFT LTDA (CNPJ: 23.303.829/0001-69)   }
{    TODOS OS DIREITOS RESERVADOS.                                           }
{    +55 31 35050151  |  infra@nacionalsoft.com.br  |  BRASIL/BRAZIL         }
{                                                                            }
{ ========================================================================== }
{           ESTE TEXTO FOI ESCRITO SEM ACENTUACAO PROPOSITALMENTE            }
{ ========================================================================== }
{                                                                            }
{ TODO O CONTEUDO DESTE ARQUIVO E DE ARQUIVOS RELACIONADOS EH PROTEGIDO POR  }
{ LEIS BRASILEIRAS E INTERNACIONAIS DE DIREITOS AUTORAIS, SENDO VETADA SUA   }
{ DISTRIBUICAO, COMERCIALIZACAO, DIVULGACAO, ENGENHARIA-REVERSA OU QUALQUER  }
{ OUTRA FORMA DE DISPONIBILIZACAO, PARA QUAISQUER FINALIDADES, SEJA EM SUA   }
{ TOTALIDADE OU EM QUALQUER PARTE DE SEU CONTEUDO.                           }
{                                                                            }
{ AVISO DE DIREITOS AUTORAIS                                                 }
{ ==========================                                                 }
{                                                                            }
{ ESTE CODIGO FONTE E TODOS OS ARQUIVOS INTERMEDIARIOS DELE RESULTANTES SAO  }
{ INTEIRAMENTE CONFIDENCIAIS E REPRESENTAM SEGREDO INDUSTRIAL E COMERCIAL DE }
{ SUA PRODUTORA (NACIONAL SOFT LTDA).                                        }
{                                                                            }
{ O CONTEUDO DESTE ARQUIVO E DE TODOS OS ARQUIVOS RELACIONADOS, EM TODO OU   }
{ EM PARTE, NAO PODERA SER COPIADO, TRANSFERIDO, COMERCIALIZADO, DISTRIBUIDO }
{ OU DISPONIBILIZADO SOB QUALQUER FORMA SEM O PREVIO CONSENTIMENTO, POR      }
{ ESCRITO, DA NACIONAL SOFT LTDA, CONCEDENDO TODAS AS PERMISSOES NECESSARIAS }
{ PARA TAL.                                                                  }
{                                                                            }
{ O DESCUMPRIMENTO DESTE PODERA ACARRETAR AO INFRATOR OU EMPRESA RESPONSAVEL }
{ PELA VIOLACAO OU AMBOS EM PROCESSOS CIVIS, CRIMINAIS, ADMINISTRATIVOS E DE }
{ PERDAS FINANCEIRAS, TODOS EM SUA MAXIMA PENALIDADE, POR SER CONSIDERADO UM }
{ CRIME DE VIOLACAO DE SEGREDO COMPETITIVO E FINANCEIRO DA PRODUTORA.        }
{                                                                            }
{ AVISO AOS DESENVOLVEDORES DA NACIONAL SOFT                                 }
{ ==========================================                                 }
{                                                                            }
{ MANTENHA-SE INFORMADO SOBRE AS RESTRICOES DE USO E POLITICAS DE SEGURANCA  }
{ DA EMPRESA. NAO REPASSE ESTE ARQUIVO A OUTROS, MESMO QUE SEJAM FUNCIONARI- }
{ OS ATIVOS DA NACIONAL SOFT. NEM TODOS OS FUNCIONARIOS POSSUEM PERMISSAO DE }
{ ACESSO AOS CODIGOS FONTES DE NOSSOS SISTEMAS.                              }
{                                                                            }
*****************************************************************************/
#endregion COPYRIGHT(C) 2014 NACIONAL SOFT LTDA
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTables.Mvc
{
    /// <summary>
    /// Provides extension methods for use with NameValueCollections.
    /// </summary>
    public static class NameValueCollectionExtensions
    {
        /// <summary>
        /// Gets a typed item from the collection using the provided key.
        /// If there's no corresponding item on the collection, returns default(T).
        /// </summary>
        /// <typeparam name="T">The type to cast the collection item.</typeparam>
        /// <param name="collection">The collection.</param>
        /// <param name="key">The key to access the item inside the collection.</param>
        /// <returns>The typed item.</returns>
        public static T G<T>(this NameValueCollection collection, string key) { return G<T>(collection, key, default(T)); }
        /// <summary>
        /// Gets a typed item from the collection using the provided key.
        /// If there's no corresponding item on the collection, returns the provided defaultValue.
        /// </summary>
        /// <typeparam name="T">The type to cast the collection item.</typeparam>
        /// <param name="collection">The collection.</param>
        /// <param name="key">The key to access the item inside the collection.</param>
        /// <param name="defaultValue">The default value to return if there's no corresponding item on the collection.</param>
        /// <returns>The typed item.</returns>
        public static T G<T>(this NameValueCollection collection, string key, object defaultValue)
        {
            if (collection == null) throw new ArgumentNullException("collection", "The provided collection cannot be null.");
            if (String.IsNullOrWhiteSpace(key)) throw new ArgumentException("The provided key cannot be null or empty.", "key");

            var collectionItem = collection[key];
            if (collectionItem == null) return (T)defaultValue;
            return (T)Convert.ChangeType(collectionItem, typeof(T));
        }
    }
}
