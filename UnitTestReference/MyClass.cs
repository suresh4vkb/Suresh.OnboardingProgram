namespace UnitTestReference
{
    public class MyClass
    {
        private readonly IService _service;
        public MyClass(IService service)
        {
            _service = service;
        }

        public string WhatItReturns(int? a) {
           return _service.ReturnAorB(a);
        }
    }
}
