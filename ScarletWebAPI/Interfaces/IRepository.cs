using System;
using System.Collections;
using System.Collections.Generic;

namespace ScarletWebAPI.Interfaces;

public interface IRepository<T>
{
	IEnumerable<T> Get();
	T GetById(int id);
	void Update(T value);
	void Add (T value);
	void Delete(int id);
}