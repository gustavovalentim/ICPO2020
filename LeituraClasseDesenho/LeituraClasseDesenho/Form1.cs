using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Collections;

namespace LeituraClasseDesenho
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnLer_Click(object sender, EventArgs e)
        {
            ProblemaTSPLIB Problema = new ProblemaTSPLIB();
            Problema.LerArquivo(@"C:\Teste\TSP_a280.txt");
            Problema.CalcularDistanciaEuclidianaTodosPontos();
            //Problema.Dimension = 7;
            List<int[]> MinhaArvore = Problema.EncontrarMinimaArborescenciaBoruvka();
            //List<int[]> MinhaArvore = Problema.EncontrarMinimaArborescenciaPrim();
            picDesenho.Image = Problema.DesenharArvore(MinhaArvore);
            double CustoTotal = 0;
            foreach(int[] aresta in MinhaArvore)
            {
                CustoTotal += Problema.MatrizDistanciasEuclidiana[aresta[0], aresta[1]];
            }
            MessageBox.Show(CustoTotal.ToString());
        }

        private void btnDesenhar_Click(object sender, EventArgs e)
        {
            Graphics g = picDesenho.CreateGraphics();
            Brush Pincel = new SolidBrush(Color.Blue);
            g.FillRectangle(Pincel, 10, 10, 50, 50);
            g.FillEllipse(Pincel, 80, 80, 30, 30);
        }
    }
    public class ProblemaTSPLIB
    {
        public string Name;
        public string Comment;
        public string Type;
        public int Dimension;
        public string EdgeWeightType;
        public string Estrutura;
        public No[] Nos;
        public double[,] MatrizDistanciasEuclidiana;
        public void LerArquivo(string _Caminho)
        {
            string[] Leitura = File.ReadAllLines(_Caminho);
            string[] PropriedadeName = Leitura[0].Split(':');
            Name = PropriedadeName[1].Trim();
            string[] PropriedadeComment = Leitura[1].Split(':');
            Comment = PropriedadeComment[1].Trim();
            string[] PropriedadeType = Leitura[2].Split(':');
            Type = PropriedadeType[1].Trim();
            string[] PropriedadeDimension = Leitura[3].Split(':');
            Dimension = int.Parse(PropriedadeDimension[1].Trim());
            string[] PropriedadeEdgeWeightType = Leitura[4].Split(':');
            EdgeWeightType = PropriedadeEdgeWeightType[1].Trim();
            Estrutura = Leitura[5];
            Nos = new No[Dimension];
            for (int i = 0; i < Dimension; i++)
            {
                string[] Atual = Leitura[i + 6].Trim().Replace("   ", " ").Replace("  ", " ").Split(' ');
                Nos[i] = new No();
                Nos[i].Numero = int.Parse(Atual[0]);
                Nos[i].CoordenadaX = double.Parse(Atual[1]);
                Nos[i].CoordenadaY = double.Parse(Atual[2]);
            }
        }
        public double CalcularDistanciaEuclidiana2Pontos(double x1, double y1, double x2, double y2)
        {
            double DeltaX = x2 - x1;
            double DeltaY = y2 - y1;
            double DistanciaEuclidiana = Math.Sqrt(DeltaX * DeltaX + DeltaY * DeltaY);
            return DistanciaEuclidiana;
        }
        public double CalcularDistanciaEuclidiana2Pontos(No No1, No No2)
        {
            double DeltaX = No2.CoordenadaX - No1.CoordenadaX;
            double DeltaY = No2.CoordenadaY - No1.CoordenadaY;
            double DistanciaEuclidiana = Math.Sqrt(DeltaX * DeltaX + DeltaY * DeltaY);
            return DistanciaEuclidiana;
        }
        public void CalcularDistanciaEuclidianaTodosPontos()
        {
            MatrizDistanciasEuclidiana = new double[Dimension, Dimension];
            for(int i=0;i<Dimension;i++)
            {
                for(int j=0;j<Dimension;j++)
                {
                    MatrizDistanciasEuclidiana[i, j] = CalcularDistanciaEuclidiana2Pontos(Nos[i], Nos[j]);
                }
            }
        }
        public Bitmap DesenharArvore(List<int[]> arvoreDesenho)
        {
            Bitmap Desenho = new Bitmap(300, 300);
            Graphics g = Graphics.FromImage(Desenho);
            foreach(No noAtual in Nos)
            {
                Brush Pincel = new SolidBrush(Color.Green);
                double Raio = 2;
                g.FillEllipse(Pincel, (float)(noAtual.CoordenadaX - Raio), (float)(noAtual.CoordenadaY - Raio), (float)(2 * Raio), (float)(2 * Raio));
            }
            foreach(int[] aresta in arvoreDesenho)
            {
                int p1 = aresta[0];
                int p2 = aresta[1];
                g.DrawLine(Pens.Blue, (float)Nos[p1].CoordenadaX, (float)Nos[p1].CoordenadaY, (float)Nos[p2].CoordenadaX, (float)Nos[p2].CoordenadaY);
            }
            return Desenho;
        }
        public List<int[]> EncontrarMinimaArborescenciaPrim()
        {
            //inicializar variáveis
            List<VerticeNaoConectado> VerticesNaoConectados = new List<VerticeNaoConectado>();
            List<int[]> Arvore = new List<int[]>();
            //Escolher Vertice Inicial Arbitrário
            int VerticeInicial = 0;
            for(int i=1;i<Dimension;i++)
            {
                VerticeNaoConectado v = new VerticeNaoConectado();
                v.Vertice = i;
                v.Distancia = MatrizDistanciasEuclidiana[VerticeInicial, i];
                v.VizinhoMaisProximo = VerticeInicial;
                VerticesNaoConectados.Add(v);
            }
            //Laço principal
            while(VerticesNaoConectados.Count>0)
            {
                //Encontrar Vertice não conectado mais próximo de algum vértice já conectado
                int PosicaoVerticeMaisPerto = -1;
                double MenorDistancia = double.MaxValue;
                for(int i=0; i < VerticesNaoConectados.Count; i++)
                {
                    if(VerticesNaoConectados[i].Distancia<MenorDistancia)
                    {
                        MenorDistancia = VerticesNaoConectados[i].Distancia;
                        PosicaoVerticeMaisPerto = i;
                    }
                }
                int VerticeConectado = VerticesNaoConectados[PosicaoVerticeMaisPerto].VizinhoMaisProximo;
                int VerticeParaConectar = VerticesNaoConectados[PosicaoVerticeMaisPerto].Vertice;
                //Adicionar aresta à árvore
                Arvore.Add(new int[2] {VerticeConectado, VerticeParaConectar});
                //Atualizar Vértices Não Conectados
                VerticesNaoConectados.RemoveAt(PosicaoVerticeMaisPerto);
                foreach(VerticeNaoConectado v in VerticesNaoConectados)
                {
                    if(MatrizDistanciasEuclidiana[VerticeParaConectar, v.Vertice] <v.Distancia)
                    {
                        v.Distancia = MatrizDistanciasEuclidiana[VerticeParaConectar, v.Vertice];
                        v.VizinhoMaisProximo = VerticeParaConectar;
                    }
                }
            }
            return Arvore;
        }
        class VerticeNaoConectado
        {
            public int Vertice;
            public double Distancia;
            public int VizinhoMaisProximo;
        }
        public List<int[]> EncontrarMinimaArborescenciaBoruvka()
        {
            //é possível criar um método dentro de um método
            //o método EncontrarMaisProximo existirá dentro do contexto de EncontrarMinimaArborescenciaBoruvka
            void EncontrarMaisProximo(Grupo[] gruposIteracao, int[] gruposAtivosIteracao, int grupoAtual, out int[] arestaMenorCusto)
            {
                double MenorCusto = double.MaxValue;
                arestaMenorCusto = new int[2];
                foreach (int GrupoComparacao in gruposAtivosIteracao)
                {
                    if(GrupoComparacao!=grupoAtual)
                    {
                        foreach (int PontoGrupoAtual in gruposIteracao[grupoAtual].Pontos)
                        {
                            foreach (int PontoGrupoComparacao in gruposIteracao[GrupoComparacao].Pontos)
                            {
                                if(MatrizDistanciasEuclidiana[PontoGrupoAtual,PontoGrupoComparacao]<MenorCusto)
                                {
                                    MenorCusto = MatrizDistanciasEuclidiana[PontoGrupoAtual, PontoGrupoComparacao];
                                    arestaMenorCusto[0] = PontoGrupoAtual;
                                    arestaMenorCusto[1] = PontoGrupoComparacao;
                                }
                            }
                        }
                    }
                }
            }
            //o método AtualizarGrAtualizado existirá dentro do contexto de EncontrarMinimaArborescenciaBoruvka
            void AtualizarGrAtualizado(List<int> listaAtualizar, ref int[] grAtualizado, int grupoNovo)
            {
                for(int i=0;i<listaAtualizar.Count;i++)
                {
                    grAtualizado[listaAtualizar[i]] = grupoNovo;
                }
            }
            //inicialização de variáveis
            List<int[]> Arvore = new List<int[]>();
            int[] GrAtualizado = new int[Dimension];
            List<int> GruposAtivos = new List<int>();
            Grupo[] Grupos=new Grupo[Dimension];
            for(int i=0;i<Dimension;i++)
            {
                GruposAtivos.Add(i);
                GrAtualizado[i] = i;
                Grupos[i] = new Grupo();
                Grupos[i].Pontos = new List<int>();
                Grupos[i].Pontos.Add(i);
            }
            //ínicio do programa principal
            while (GruposAtivos.Count>1)
            {
                int[] GruposAtivosInicioIteracao=new int[GruposAtivos.Count];
                GruposAtivos.CopyTo(GruposAtivosInicioIteracao);
                foreach (int GrupoAtual in GruposAtivosInicioIteracao)
                {
                    int[] ArestaMenorCusto;
                    EncontrarMaisProximo(Grupos, GruposAtivosInicioIteracao, GrupoAtual, out ArestaMenorCusto);
                    int GrupoMaisProximo = GrAtualizado[ArestaMenorCusto[1]];
                    if(GrAtualizado[GrupoAtual]!=GrAtualizado[GrupoMaisProximo])
                    {
                        GruposAtivos.Remove(GrAtualizado[GrupoMaisProximo]);
                        Grupos[GrAtualizado[GrupoAtual]].Pontos.AddRange(Grupos[GrAtualizado[GrupoMaisProximo]].Pontos);
                        AtualizarGrAtualizado(Grupos[GrAtualizado[GrupoMaisProximo]].Pontos, ref GrAtualizado, GrAtualizado[GrupoAtual]);
                        Arvore.Add(ArestaMenorCusto);
                    }
                }                
            }
            return Arvore;
            //fim do programa principal
        }
        class Grupo
        {
            public List<int> Pontos;
            public void CopiarPara(ref List<int> _Pontos)
            {
                _Pontos = new List<int>();
                foreach(int ponto in Pontos)
                {
                    _Pontos.Add(ponto);
                }
            }
        }
    }
    public class No
    {
        public int Numero;
        public double CoordenadaX;
        public double CoordenadaY;
    }
}
