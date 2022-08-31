using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace WorkflowCore.Test.Model
{

    /// <summary>
    /// saga回滚时执行 补偿方法
    /// </summary>
    public class CompensateWithModel
    {
        /// <summary>
        /// id
        /// </summary>
        [Required]
        public string Id { get; set; }
        /// <summary>
        /// 方法名反射
        /// 生成方法 $"{typeof(步骤class).FullName}, {typeof(步骤class).Assembly.FullName}"
        /// </summary>
        [Required]
        public string StepType { get; set; }
        /// <summary>
        /// 容器内下一个步骤id
        /// </summary>
        public string NextStepId { get; set; } = "";
        /// <summary>
        /// 发生错误后
        /// 例如
        /// WorkflowErrorHandling.Retry.ToString()
        /// </summary>
        public string ErrorBehavior { get; set; } = null;
        /// <summary>
        /// 发生错误后执行时间
        /// 例如："00:00:05"
        /// </summary>
        public string RetryInterval { get; set; } = null;
    }
}
