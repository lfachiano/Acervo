namespace Acervo.DTOS.Livro
{
    public class LivroFilterDTO
    {
        public string? Titulo { get; set; }
        public int? AnoPublicacao { get; set; }
        public int? AnoInicio { get; set; }
        public int? AnoFim { get; set; }
        public string? OrdenarPor { get; set; }

    }
}
