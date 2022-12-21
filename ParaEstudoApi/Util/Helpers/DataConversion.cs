namespace ParaEstudoApi.Util.Helpers
{
    public static class DataConversion
    {
        /// <summary>
        /// Faz a conversão de tamanho byte para kB(kilo byte).
        /// </summary>
        /// <param name="byteValue"></param>
        /// <returns></returns>
        public static long ByteToKiloByte(long byteValue)
        {
            return byteValue / 1024;

        }

        /// <summary>
        /// Faz a conversão de Kb(kilo byte) em MB(mega byte).
        /// </summary>
        /// <param name="kbValue"></param>
        /// <returns></returns>
        public static long KiloByteToMegaByte(long kbValue)
        {
            return kbValue / 1024;
        }
    }
}
