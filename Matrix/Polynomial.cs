using System;
using System.Collections.Generic;
using System.Text;

namespace Practice1
{
    public class Polynomial
    {
        private Dictionary<int, int> polynomial;

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
                polynomial = new Dictionary<int, int>();
                
                for (int i = 0; i < powers.Length; i++)
                {
                    try
                    {
                        polynomial.Add(powers[i], coefficients[i]);
                    }
                    catch (ArgumentException ex)
                    {
                        throw new ArgumentException($"An element with power {powers[i]} " +
                                          $"already exists. " +
                                          $"Note that powers must be unique", ex);
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
                this.polynomial = polynomial.GetPolynomial();
            }
        }

        public Polynomial()
        {
            polynomial = new Dictionary<int, int>();
        }

        public static Polynomial operator +(Polynomial polynomial, int n)
        {
            if (polynomial != null)
            {
                Polynomial result = new Polynomial();
                foreach (var kvp in polynomial.GetPolynomial())
                {
                    result.polynomial.Add(kvp.Key, kvp.Value + n);
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
                foreach (var kvp in polynomial.polynomial)
                {
                    result.polynomial.Add(kvp.Key, kvp.Value * n);
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

                foreach (var kvp1 in polynomial1.polynomial)
                {
                    foreach (var kvp2 in polynomial2.polynomial)
                    {
                        int powSum = kvp1.Key + kvp2.Key;
                        if (result.polynomial.TryGetValue(powSum, out int value))
                        {
                            result.polynomial[powSum] = value + (kvp1.Value * kvp2.Value);
                        }
                        else
                        {
                            result.polynomial.Add(powSum, kvp1.Value * kvp2.Value);
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
            foreach (var kvp1 in this.polynomial)
            {
                foreach (var kvp2 in polynomial.polynomial)
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
    }
}
