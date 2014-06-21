using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace core
{
	public class State
	{
        public State(Action start, Action<float> run, Action end)
        {
            _start = start;
            _run = run;
            _end = end;
        }

        public void Start()
        {
            if (_start != null)
            {
                _start();
            }
        }

        public void Run(float deltaTime)
        {
            if (_run != null)
            {
                _run(deltaTime);
            }
        }

        public void End()
        {
            if (_end != null)
            {
                _end();
            }
        }

        private Action _start;
        private Action<float> _run;
        private Action _end;
	}
}
