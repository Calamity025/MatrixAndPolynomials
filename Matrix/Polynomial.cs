using System;
using System.Collections.Generic;
using System.Text;

namespace Practice1
{
    public class Polynomial
    {
        public Dictionary<int, int> IntPolynomial { get; private set; }

        public Polynomial(int[] powers, int[] coefficients)
        {
            if (powers == null || coefficients == null)
            {
                throw new ArgumentNullException();
            }
            else if (powers.Length != coefficients.Length)
            {
                throw new ArgumentException("Length of powers array must be equals to length of coefficients array");
            }
            else
            {
                IntPolynomial = new Dictionary<int, int>();
                
                for (int i = 0; i < powers.Length; i++)
                {
                    try
                    {
                        IntPolynomial.Add(powers[i], coefficients[i]);
                    }
                    catch (ArgumentException)
                    {
                        Console.WriteLine($"An element with power {powers[i]} " +
                                          $"already exists. " +
                                          $"Note that powers must be unique");

                        throw;
                    }
                }
            }
        }
        

        public Polynomial(Polynomial polynomial)
        {
            if (polynomial == null)
            {
                throw new ArgumentNullException();
            }
            else
            {
                IntPolynomial = polynomial.IntPolynomial;
            }
        }

        public Polynomial()
        {
            IntPolynomial = new Dictionary<int, int>();
        }

        public static Polynomial operator +(Polynomial polynomial, int n)
        {
            if (polynomial != null)
            {
                Polynomial result = new Polynomial();
                foreach (var kvp in polynomial.IntPolynomial)
                {
                    result.IntPolynomial.Add(kvp.Key, kvp.Value + n);
                }

                return result;
            }
            else
            {
                throw new ArgumentNullException();
            }
        }

        public static Polynomial operator -(Polynomial polynomial, int n)
        {
            return new Polynomial(polynomial + n*-1);
        }

        public static Polynomial operator *(Polynomial polynomial, int n)
        {
            if (polynomial != null)
            {
                Polynomial result = new Polynomial();
                foreach (var kvp in polynomial.IntPolynomial)
                {
                    result.IntPolynomial.Add(kvp.Key, kvp.Value * n);
                }

                return result;
            }
            else
            {
                throw new ArgumentNullException();
            }
        }

        public static Polynomial operator *(Polynomial polynomial1, Polynomial polynomial2)
        {
            if (polynomial1 != null || polynomial2 != null)
            {
                Polynomial result = new Polynomial();

                foreach (var kvp1 in polynomial1.IntPolynomial)
                {
                    foreach (var kvp2 in polynomial2.IntPolynomial)
                    {
                        int powSum = kvp1.Key + kvp2.Key;
                        if (result.IntPolynomial.TryGetValue(powSum, out int value))
                        {
                            result.IntPolynomial[powSum] = value + (kvp1.Value * kvp2.Value);
                        }
                        else
                        {
                            result.IntPolynomial.Add(powSum, kvp1.Value * kvp2.Value);
                        }
                    }
                }

                return result;
            }
            else
            {
                throw new ArgumentNullException();
            }
        }

        public override bool Equals(Object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }
            Polynomial polynomial = (Polynomial) obj;
            foreach (var kvp1 in IntPolynomial)
            {
                foreach (var kvp2 in polynomial.IntPolynomial)
                {
                    if (kvp1.Key == kvp2.Key)
                    {
                        if (kvp1.Value != kvp2.Value)
                        {
                            return false;
                        }
                    }
                }
            }

            return true;
        }

        public Polynomial Copy()
        {
            return new Polynomial(this);
        }
    }
}
