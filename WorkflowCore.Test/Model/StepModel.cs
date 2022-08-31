using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace WorkflowCore.Test.Model
{

    /// <summary>
    /// 步骤model
    /// </summary>
    public class StepModel
    {
        /// <summary>
        /// 步骤id
        /// </summary>
        [Required]
        public string Id { get; set; }
        /// <summary>
        /// 步骤的方法名反射
        /// 生成方法 $"{typeof(步骤class).FullName}, {typeof(步骤class).Assembly.FullName}"
        /// </summary>
        [Required]
        public string StepType { get; set; }
        /// <summary>
        /// 下一个步骤id
        /// </summary>
        public string NextStepId { get; set; } = "";
        /// <summary>
        /// 输入的字段，可以填充ActivitySetpInputModel
        /// </summary>
        public object Inputs { get; set; } = null;
        /// <summary>
        /// 输出的字段
        /// </summary>
        public object Outputs { get; set; } = null;

        /// <summary>
        /// 步骤容器
        /// true情况下需要将StepType设置为 "WorkflowCore.Primitives.Sequence, WorkflowCore",
        /// </summary>
        public bool Saga { get; set; }

        public List<List<SagaDoModel>> Do { get; set; } = null;
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
