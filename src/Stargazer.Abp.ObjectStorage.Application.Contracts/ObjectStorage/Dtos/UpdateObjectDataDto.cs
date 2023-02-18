namespace Stargazer.Abp.ObjectStorage.Application.Contracts.ObjectStorage.Dtos
{
    public class UpdateObjectDataDto
    {
        /// <summary>
        /// 文件类型
        /// </summary>
        public string FileType { get; set; } = "";

        /// <summary>
        /// 文件扩展名
        /// </summary>
        public string FileExtension { get; set; }
        
        /// <summary>
        /// 文件的hash值
        /// </summary>
        public string FileHash { get; set; } = "";

        /// <summary>
        /// 文件保存路径
        /// </summary>
        public string FilePath { get; set; } = "";

        /// <summary>
        /// 
        /// </summary>
        public long FileSize { get; set; }

    }
}