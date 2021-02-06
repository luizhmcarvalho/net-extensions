namespace System
{
    public enum EModeDiffDate
    {
        /// <summary>
        ///     Calcula diferença de datas por dias corridos.
        /// </summary>
        AllDays = 1,

        /// <summary>
        ///     Calcula diferença de datas por dias úteis (não conta finais de semana e feriados).
        /// </summary>
        WorkDays = 2,

        /// <summary>
        ///     Calcula diferença de datas considerando mês de 30 dias (dia 31 não será contato,
        ///     dia 28 de fevereiro de ano comum será contato como 3 dias, e dia 28 de fevereiro
        ///     de ano bissexto será contato como 2 dias).
        /// </summary>
        WorkMonth = 3
    }
}