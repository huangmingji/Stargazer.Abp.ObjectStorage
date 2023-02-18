using System;

namespace Stargazer.Abp.ObjectStorage.Application.Contracts.ObjectStorage.Dtos
{
    public class ObjectDataDto
    {
        public Guid Id { get; set; }
        
        public Guid? TenantId { get; set; }

        /// <summary>
        /// 对象类型
        /// </summary>
        public string ObjectType { get; set; } = "";

        /// <summary>
        /// 对象扩展名
        /// </summary>
        public string ObjectExtension { get; set; }
        
        /// <summary>
        /// 对象的hash值
        /// </summary>
        public string ObjectHash { get; set; } = "";

        /// <summary>
        /// 对象保存路径
        /// </summary>
        public string ObjectPath { get; set; } = "";
        
        /// <summary>
        /// 对象大小（kb）
        /// </summary>
        public int ObjectSize { get; set; }
    }
}