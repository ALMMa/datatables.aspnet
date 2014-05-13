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
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTables.Mvc
{
    /// <summary>
    /// Implements a default DataTables request.
    /// </summary>
    public class DefaultDataTablesRequest : IDataTablesRequest
    {
        /// <summary>
        /// Gets/Sets the draw counter from DataTables.
        /// </summary>
        public virtual int Draw { get; set; }
        /// <summary>
        /// Gets/Sets the start record number (jump) for paging.
        /// </summary>
        public virtual int Start { get; set; }
        /// <summary>
        /// Gets/Sets the length of the page (paging).
        /// </summary>
        public virtual int Length { get; set; }
        /// <summary>
        /// Gets/Sets the global search term.
        /// </summary>
        public virtual Search Search { get; set; }
        /// <summary>
        /// Gets/Sets the column collection.
        /// </summary>
        public virtual ColumnCollection Columns { get; set; }
    }
}
