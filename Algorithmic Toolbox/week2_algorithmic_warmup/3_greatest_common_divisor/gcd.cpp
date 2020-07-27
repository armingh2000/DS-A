#include <iostream>

int gcd_naive(int a, int b) {
  int current_gcd = 1;
  for (int d = 2; d <= a && d <= b; d++) {
    if (a % d == 0 && b % d == 0) {
      if (d > current_gcd) {
        current_gcd = d;
      }
    }
  }
  return current_gcd;
}

int gcd_fast(int a, int b)
{
    int m = std::max(a, b);
    int s = std::min(a, b);
    
    if(m % s == 0)
        return s;

    int temp;
    while(m % s != 0)
    {
        temp = m % s;
        m = s;
        s = temp;
    }
    return s;
}

int main() {
  int a, b;
  std::cin >> a >> b;
  //std::cout << gcd_naive(a, b) << std::endl;
  std::cout << gcd_fast(a, b) << std::endl;
  return 0;
}
