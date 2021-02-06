namespace System
{
    public class Masks
    {
        /// <summary>
        ///     Máscara de CPF '999.999.999-99'
        /// </summary>
        public static Masks CPF => new Masks("999\\.999\\.999-99");

        /// <summary>
        ///     Máscara de CNPJ '99.999.999/9999-99'
        /// </summary>
        public static Masks CNPJ => new Masks("99\\.999\\.999\\/9999-99");

        /// <summary>
        ///     Máscara de PIS '999.99999.99-9'
        /// </summary>
        public static Masks Invoice => new Masks("9999.9999999-9");

        /// <summary>
        ///     Máscara de Número Certificado '9999.9999999-9'
        /// </summary>
        public static Masks PIS => new Masks("999\\.99999\\.99-9");

        /// <summary>
        ///     Máscara de CNPB '9999.9999-99'
        /// </summary>
        public static Masks CNPB => new Masks("9999\\.9999-99");

        /// <summary>
        ///     Máscara de CEP '99999-999'
        /// </summary>
        public static Masks CEP => new Masks("99999-999");

        /// <summary>
        ///     Máscara de CNAE '99.99-9-99'
        /// </summary>
        public static Masks CNAE => new Masks("99\\.99\\-9\\-99");

        /// <summary>
        ///     Máscara de IBGE-Cidade '99-99.999'
        /// </summary>
        public static Masks IBGE => new Masks("99\\-99\\.999");

        /// <summary>
        ///     Máscara de SUSEP '99999.999999/9999-99'
        /// </summary>
        public static Masks SUSEP => new Masks("99999\\.999999\\/9999-99");

        /// <summary>
        ///     Máscara de NIRE '99-9-9999999-9'
        /// </summary>
        public static Masks NIRE => new Masks("99-9-9999999-9");

        /// <summary>
        ///     Máscara de Matrícula do Participante ''
        /// </summary>
        public static Masks MATRÍCULA => new Masks("");

        /// <summary>
        ///     Máscara de Número do Protocolo '999.999999.9999-9'
        /// </summary>
        public static Masks PROTOCOLO => new Masks("999\\.999999\\.9999-9");

        /// <summary>
        ///     Máscara de Número do INSS '999.999.999-9'
        /// </summary>
        public static Masks INSS => new Masks("999\\.999\\.999-9");

        /// <summary>
        ///     Máscara de Linha Digitável para Boleto Bancário
        /// </summary>
        public static Masks BoletoBancario => new Masks(@"99999\.99999 99999\.999999 99999\.999999 9 99999999999999");

        /// <summary>
        ///     Máscara de Linha Digitável para Boleto de Contas de Consumo e Tributos
        /// </summary>
        public static Masks BoletoConsumo => new Masks(@"99999999999\.9 99999999999\.9 99999999999\.9 99999999999\.9");

        /// <summary>
        ///     Máscara de Matrícula da Carta de Cobranca '99.999.999-9'
        /// </summary>
        public static Masks CartaCobrancaMatricula => new Masks("99\\.999\\.999-9");

        /// <summary>
        ///     Máscara com ponto no CEP '99.999-999'
        /// </summary>
        public static Masks CEP2 => new Masks("99\\.999-999");

        /// <summary>
        ///     Máscara numero celular '(99)99999-9999'
        /// </summary>
        public static Masks CELULAR => new Masks("\\(99)99999-9999");

        /// <summary>
        ///     Máscara numero telefone '(99)9999-9999'
        /// </summary>
        public static Masks TELEFONE => new Masks("\\(99)9999-9999");

        /// <summary>
        ///     Cast para capturar a string de máscara equivalente
        /// </summary>
        /// <param name="pMask">O objeto Masks</param>
        /// <returns></returns>
        public static implicit operator string(Masks pMask)
        {
            return pMask.ToString();
        }

        /// <summary>
        ///     Retorna a máscara para determinado tipo de máscara
        /// </summary>
        /// <param name="type">O tipo da máscara</param>
        /// <returns>máscara do tipo de máscara correspondente</returns>
        public static Masks GetMask(MaskTypes type)
        {
            switch (type)
            {
                case MaskTypes.CPF: return CPF;
                case MaskTypes.CNPJ: return CNPJ;
                case MaskTypes.PIS: return PIS;
                case MaskTypes.CNPB: return CNPB;
                case MaskTypes.CEP: return CEP;
                case MaskTypes.CNAE: return CNAE;
                case MaskTypes.IBGE_Cidade: return IBGE;
                case MaskTypes.SUSEP: return SUSEP;
                case MaskTypes.NumCertificado: return Invoice;
                case MaskTypes.NIRE: return NIRE;
                case MaskTypes.PROTOCOLO: return PROTOCOLO;
                case MaskTypes.MATRÍCULA: return MATRÍCULA;
                case MaskTypes.INSS: return INSS;
                case MaskTypes.BoletoBancario: return BoletoBancario;
                case MaskTypes.BoletoConsumo: return BoletoConsumo;
                case MaskTypes.CartaCobrancaMatricula: return CartaCobrancaMatricula;
                case MaskTypes.CEP2: return CEP2;
                case MaskTypes.CELULAR: return CELULAR;
                case MaskTypes.TELEFONE: return TELEFONE;
                default: return new Masks("");
            }
        }

        #region Instance

        private Masks()
        {
        }

        private Masks(string pMask)
        {
            _value = pMask;
        }

        private readonly string _value;

        /// <summary>
        ///     Retorna a string de máscara equivalente
        /// </summary>
        /// <returns>String com a máscara</returns>
        public override string ToString()
        {
            return _value;
        }

        #endregion
    }

    public enum MaskTypes
    {
        None,
        CPF,
        CNPJ,
        PIS,
        CNPB,
        CEP,
        CNAE,
        IBGE_Cidade,
        NumCertificado,
        SUSEP,
        NIRE,
        MATRÍCULA,
        PROTOCOLO,
        INSS,
        BoletoBancario,
        BoletoConsumo,
        CartaCobrancaMatricula,
        CEP2,
        CELULAR,
        TELEFONE
    }
}