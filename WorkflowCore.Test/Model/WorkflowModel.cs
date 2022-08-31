using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using WorkflowCore.Models;

namespace WorkflowCore.Test.Model
{
    public class WorkflowModel
    {
        /// <summary>
        ///模板id
        /// </summary>
        [Required]
        public string Id { get; set; }
        [Required]
        public int Version { get; set; } = 1;
        //报表数据格式的反射字段名称，例："MyApp.MyDataClass, MyApp"
        //生成方法 $"{typeof(Dictionary<string, object>).FullName}, {typeof(Dictionary<string, object>).Assembly.FullName}"
        public string DataType { get; set; } = "";
        //步骤
        public StepModel[] Steps { get; set; } = null;
    }

}
