namespace WEB.ViewsModels.PersonagemViewModel
{
    public class DetalhesDoPersonagemViewModel
    {
        public DetalhesDoPersonagemViewModel(
            int id, 
            string nome, 
            string descricao, 
            string path, 
            string extension)
        {
            Id = id;
            Nome = nome;
            Descricao = descricao;
            if (!string.IsNullOrEmpty(path) && !string.IsNullOrEmpty(extension))
                Imagem = $"{path}.{extension}";
        }

        public int? Id { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public string Imagem { get; set; }
    }
}
