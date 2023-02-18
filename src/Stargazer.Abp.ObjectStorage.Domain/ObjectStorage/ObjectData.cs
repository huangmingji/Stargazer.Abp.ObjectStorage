using System;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;

namespace Stargazer.Abp.ObjectStorage.Domain
{
    public class ObjectData : AuditedEntity<Guid>, IMultiTenant
    {
        public ObjectData()
        {
        }

        public ObjectData(Guid id, 
            string objectType, 
            string objectExtension, 
            string objectHash, 
            string objectPath,
            long objectSize)
            : base(id)
        {
            Check.NotNullOrWhiteSpace(objectType, nameof(objectType));
            Check.NotNullOrWhiteSpace(objectExtension, nameof(objectExtension));
            Check.NotNullOrWhiteSpace(objectHash, nameof(objectHash));
            Check.NotNullOrWhiteSpace(objectPath, nameof(objectPath));
            this.ObjectType = objectType;
            this.ObjectExtension = objectExtension;
            this.ObjectHash = objectHash;
            this.ObjectPath = objectPath;
            this.ObjectSize = objectSize;
        }

        public void Update(string objectType, 
            string objectExtension, string objectHash, string objectPath, long objectSize)
        {
            Check.NotNullOrWhiteSpace(objectHash, nameof(objectHash));
            Check.NotNullOrWhiteSpace(objectPath, nameof(objectPath));
            this.ObjectType = objectType;
            this.ObjectExtension = objectExtension;
            this.ObjectHash = objectHash;
            this.ObjectPath = objectPath;
            this.ObjectSize = objectSize;
        }

        public Guid? TenantId { get; set; }

        /// <summary>
        /// 对象类型
        /// </summary>
        public string ObjectType { get; set; } = "";

        /// <summary>
        /// 对象扩展名
        /// </summary>
        public string ObjectExtension { get; set; } = "";
        
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
        public long ObjectSize { get; set; }

    }
}