# Correctness audit log
| path | status | note |
|------|--------|------|
| src/Advanced.Algorithms/Search/BinarySearch.cs | fixed | empty input indexed `input[0]`; guard `i > j` → `-1`; oracle vs known indices |
| src/Advanced.Algorithms/Search/BoyerMoore.cs | ok | majority oracle vs `count > n/2`; no code change |
| src/Advanced.Algorithms/Search/QuickSelect.cs | ok | kth-smallest oracle vs `sorted[k-1]`; no code change |
| src/Advanced.Algorithms/Compression/HuffmanCoding.cs | ok | codebook roundtrip + prefix-free + frequency/length sanity; no decode API |
| src/Advanced.Algorithms/Sorting/BubbleSort.cs | ok | Array.Sort oracle on empty/single/dups/reverse/sorted/random |
| src/Advanced.Algorithms/Sorting/InsertionSort.cs | ok | Array.Sort oracle on shared fixtures |
| src/Advanced.Algorithms/Sorting/SelectionSort.cs | ok | Array.Sort oracle on shared fixtures |
| src/Advanced.Algorithms/Sorting/ShellSort.cs | ok | Array.Sort oracle on shared fixtures |
| src/Advanced.Algorithms/Sorting/MergeSort.cs | ok | Array.Sort oracle on shared fixtures |
| src/Advanced.Algorithms/Sorting/QuickSort.cs | ok | Array.Sort oracle on shared fixtures |
| src/Advanced.Algorithms/Sorting/HeapSort.cs | ok | Array.Sort oracle on shared fixtures |
| src/Advanced.Algorithms/Sorting/CountingSort.cs | fixed | empty input overflowed `max+1` (`int.MinValue`); early return `Array.Empty` |
| src/Advanced.Algorithms/Sorting/RadixSort.cs | fixed | empty input threw on `Max()`; early return |
| src/Advanced.Algorithms/Sorting/BucketSort.cs | fixed | `bucketSize==0` on non-empty returned zeros; reject invalid size |
| src/Advanced.Algorithms/Sorting/TreeSort.cs | ok | Array.Sort oracle on unique keys; duplicates throw by design (RB tree) |
| src/Advanced.Algorithms/Binary/GCD.cs | pass | Fixed gcd(negative,0) returning negative; Euclidean oracle tests. |
| tests/Advanced.Algorithms.Tests/Binary/GCD_Tests.cs | pass | Oracle vs Euclidean over grid and edge pairs. |
| src/Advanced.Algorithms/Binary/Logarithm.cs | pass | Fixed CalcBase10LogFloor (was floor(log2)/floor(log2(10))); FP epsilon. |
| tests/Advanced.Algorithms.Tests/Binary/Logarithm_Tests.cs | pass | Oracle vs Math.Log/Log10 for 1..2000 and adversarial samples. |
| src/Advanced.Algorithms/Binary/BaseConversion.cs | pass | No impl change; whole-number round-trips OK. |
| tests/Advanced.Algorithms.Tests/Binary/BaseConversion_Tests.cs | pass | Round-trip oracle dec<->bin/hex/base3 + known literals. |
| src/Advanced.Algorithms/Numerical/PrimeTester.cs | pass | No impl change; matches known primes. |
| tests/Advanced.Algorithms.Tests/Numerical/Primality_Tests.cs | pass | Oracle vs primes <=100 and composites beyond. |
| src/Advanced.Algorithms/Numerical/PrimeGenerator.cs | pass | No impl change; sieve matches known list / IsPrime. |
| tests/Advanced.Algorithms.Tests/Numerical/PrimeGenerator_Tests.cs | pass | Oracle list to 100; cross-check IsPrime to 200; max 0/1. |
| src/Advanced.Algorithms/Numerical/Exponentiation.cs | pass | No impl change; matches Math.Pow in int range. |
| tests/Advanced.Algorithms.Tests/Numerical/Exponentiation_Tests.cs | pass | Oracle vs Math.Pow incl. negative bases. |
| src/Advanced.Algorithms/Combinatorics/Combination.cs | pass | No impl change; counts match C(n,k) / C(n+r-1,r). |
| tests/Advanced.Algorithms.Tests/Combinatorics/Combination_Tests.cs | pass | Formula oracle n,r <=7; overflow-safe binomial helper. |
| src/Advanced.Algorithms/Combinatorics/Permutation.cs | pass | No impl change; counts match P(n,r) / n^r. |
| tests/Advanced.Algorithms.Tests/Combinatorics/Permutation_Tests.cs | pass | Formula oracle n,r <=6. |
| src/Advanced.Algorithms/Combinatorics/Subset.cs | pass | No impl change; power set size 2^n and C(n,k) sizes. |
| tests/Advanced.Algorithms.Tests/Combinatorics/Subset_Tests.cs | pass | Power-set count + per-size binomial oracle n<=10. |
| src/Advanced.Algorithms/Graph/Search/DepthFirst.cs | fixed | missed disconnected components; search all starts; empty-graph guard |
| tests/Advanced.Algorithms.Tests/Graph/Search/DepthFirst_Tests.cs | pass | disconnected-component existence oracle |

| src/Advanced.Algorithms/Geometry/PointInsidePolygon.cs | fixed | ray loop used Edges.Count-1 (skipped last edge); outside-left false positive |
| tests/Advanced.Algorithms.Tests/Geometry/PointInsidePolygon_Tests.cs | pass | square vertex oracle; known inside/outside incl. left of square |
| src/Advanced.Algorithms/Graph/Search/BreadthFirst.cs | fixed | missed disconnected components; search all starts; empty-graph guard |
| tests/Advanced.Algorithms.Tests/Graph/Search/BreadthFirst_Tests.cs | pass | disconnected-component existence oracle |
| src/Advanced.Algorithms/Graph/Search/BiDirectional.cs | fixed | reverse BFS used out-edges (false paths on digraphs); use InEdges; IDiGraph API |
| tests/Advanced.Algorithms.Tests/Graph/Search/BiDirectional_Tests.cs | pass | converging A->B<-C adversarial path oracle |
| src/Advanced.Algorithms/Graph/Sort/KahnTopSort.cs | fixed | enqueued every neighbor (dupes); incomplete result on cycles; enqueue at indegree 0 + count check |
| tests/Advanced.Algorithms.Tests/Graph/Sort/KahnTopSort_Tests.cs | pass | topo vs DAG edges oracle; diamond/cycle adversarial |
| src/Advanced.Algorithms/Graph/Sort/DepthFirstTopSort.cs | fixed | cyclic graphs returned an order; visiting-set cycle throw |
| tests/Advanced.Algorithms.Tests/Graph/Sort/DepthFirstTopSort_Tests.cs | pass | topo vs DAG edges oracle; cycle throws |
| src/Advanced.Algorithms/Graph/Cycle/CycleDetection.cs | ok | DFS visiting/visited correct; no code change |
| tests/Advanced.Algorithms.Tests/Graph/Cycle/CycleDetection_Tests.cs | pass | self-loop + DAG adversarial |
| src/Advanced.Algorithms/Graph/ArticulationPoint/TarjansArticulationFinder.cs | fixed | only scanned reference component; iterate all DFS roots |
| tests/Advanced.Algorithms.Tests/Graph/ArticulationPoints/TarjansArticulation_Tests.cs | pass | disconnected triangle+path AP oracle |
| src/Advanced.Algorithms/Graph/Bridge/TarjansBridgeFinder.cs | fixed | only scanned reference component; iterate all DFS roots |
| tests/Advanced.Algorithms.Tests/Graph/Bridge/TarjansBridge_Tests.cs | pass | disconnected triangle+edge bridge oracle |
